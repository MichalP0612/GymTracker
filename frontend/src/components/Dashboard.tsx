import {useEffect, useState} from "react";
import
{
    getExercises,
    getStatistics,
    getExerciseStatistics,
    type Exercise,
    type Statistics,
    type ExerciseStatistics,
} from "../services/api";

import ProgressChart from "./ProgressChart";

function Dashboard() {
    const [statistics, setStatistics] = useState<Statistics | null>(null);
    const [exercises, setExercises] = useState<Exercise[]>([]);
    const [selectedExerciseId, setSelectedExerciseId] = useState<number | null>(null);
    const [exerciseStatistics, setExerciseStatistics] = useState<ExerciseStatistics | null>(null);

    const [loading, setLoading] = useState(true);
    const [exerciseLoading, setExerciseLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [exerciseError, setExerciseError] = useState<string | null>(null);

    // Loads general dashboard data
    useEffect(() => {
        let cancelled = false;
        async function loadDashboard() {
            try {
                const [statisticsData, exercisesData] = await Promise.all([
                    getStatistics(),
                    getExercises()
                ]);

                if (cancelled) return;

                setStatistics(statisticsData);
                setExercises(exercisesData);

                if (exercisesData.length > 0) 
                    {
                        setSelectedExerciseId(exercisesData[0].id);
                    }
            }
            catch {
                if (!cancelled) {
                    setError("Failed to load dashboard");
                }
            }
            finally {
                if (!cancelled) 
                    {
                        setLoading(false);
                    }
            }
        }
        loadDashboard();
        return () => 
            {
                cancelled = true;
            };
    }, []);

    useEffect(() => 
        {
            if (selectedExerciseId === null) 
                {
                    setExerciseStatistics(null);
                    return;
                }

                const exerciseId = selectedExerciseId;
            
                let cancelled = false;
                async function loadExerciseStatistics() 
                {
                    try
                    {
                        setExerciseLoading(true);
                        setExerciseError(null);
                        setExerciseStatistics(null);

                        const data = await getExerciseStatistics(exerciseId);

                        if (!cancelled) 
                            {
                                setExerciseStatistics(data);
                            }
                    } catch {
                        if (!cancelled) 
                            {
                                setExerciseError("Failed to load exercise statistics");
                            }
                    } finally {
                        if (!cancelled)
                            {
                                setExerciseLoading(false);
                            }
                    }
                }
                loadExerciseStatistics();

                return () => 
                    {
                        cancelled = true;
                    };
        }, [selectedExerciseId]);

    if (loading) {
        return <p>Loading dashboard...</p>
    }

    if (error) {
        return <p>{error}</p>;
    }

    return (
        <div>
            <h1>Gym Tracker</h1>

            {statistics && (
                <div>
                    <h2>Overview</h2>
                    <p>Total Workouts: {statistics.totalWorkouts}</p>
                    <p> Total Volume: {statistics.totalVolume} kg</p>

                    <h2>Personal Records</h2>
                    {exerciseStatistics && (
                        <>
                            <h3>{exerciseStatistics.exerciseName}</h3>

                            {exerciseStatistics.progress.length > 0 ? (
                                <>
                                    <p>
                                        Best set: {exerciseStatistics.bestWeight}
                                        {" "}
                                        {exerciseStatistics.bestWeightUnit}
                                        {" x "}
                                        {exerciseStatistics.bestWeightReps}
                                    </p>

                                    <p>
                                        Estimated 1RM:{" "}
                                        {exerciseStatistics.bestEstimatedOneRepMax.toFixed(2)} kg
                                    </p>

                                    <p>
                                        Total Volume: {exerciseStatistics.totalVolume.toFixed(2)} kg
                                    </p>
                                </>
                            ) : (
                                <p>No personal records recorded yet.</p>
                            )}
                        </>
                    )}
                </div>
            )}

            <h2>Personal Records</h2>
            {statistics.personalRecords.length > 0 ? ( 
                <table>
                    <thead>
                        <tr>
                            <th>Exercise</th>
                            <th>Best set</th>
                            <th>Est. 1RM (kg)</th>
                        </tr>
                    </thead>
                    <tbody>
                        {statistics.personalRecords.map((record) => (
                            <tr key={record.exerciseId}>
                                <td>{record.exerciseName}</td>
                                <td>
                                    {record.weight} {record.unit === "Kilograms" ? "kg" : "lb"} x {record.reps}
                                </td>
                                <td>{record.estimatedOneRepMax.toFixed(2)}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <p>No personal records recorded yet.</p>
            )}

            <h2>Exercise Progress</h2>
            <select
                value={selectedExerciseId ?? ""}
                onChange={(event) => {
                    const id = Number(event.target.value);
                    setSelectedExerciseId(id);
                }}
            >
                <option value="" disabled>Select an exercise</option>

                {exercises.map((exercise) => (
                    <option key={exercise.id} value={exercise.id}>{exercise.name}</option>
                ))}
            </select>

            {exerciseLoading && <p>Loading progress...</p>}

            {exerciseError && <p>{exerciseError}</p>}

            {exerciseStatistics && (
                <>
                    <h3>{exerciseStatistics.exerciseName}</h3>

                    {exerciseStatistics.progress.length > 0 ? (
                        <>
                            <p>
                                Best set: {exerciseStatistics.bestWeight}{" "}
                                {exerciseStatistics.bestWeightUnit === "Kilograms" ? "kg" : "lb"}
                                {" x "}{exerciseStatistics.bestWeightReps}
                            </p>
                            <p>Estimated 1RM: {exerciseStatistics.bestEstimatedOneRepMax.toFixed(2)} kg</p>
                            <p>Total Volume: {exerciseStatistics.totalVolume.toFixed(2)} kg</p>

                            <ProgressChart data={exerciseStatistics.progress}/>
                        </>
                    ) : (
                        <p>No workout history for this exercise yet.</p>
                    )}
                </>
            )}
        </div>
    )
}

export default Dashboard;