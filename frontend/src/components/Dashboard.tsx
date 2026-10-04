import {useEffect, useState} from "react";
import {getStatistics, type Statistics} from "../services/api";

function Dashboard() {
    const [statistics, setStatistics] = useState<Statistics | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        async function loadStatistics() {
            try {
                const data = await getStatistics();
                setStatistics(data);
            }
            catch {
                setError("Failed to load statistics");
            }
            finally {
                setLoading(false);
            }
        }
        loadStatistics();
    }, []);

    if (loading) {
        return <p>Loading dashboard...</p>
    }

    if (error) {
        return <p>{error}</p>;
    }

    if (!statistics) {
        return <p>No statistics available</p>;
    }

    return (
        <div>
            <h1>Gym Tracker</h1>

            <div>
                <h2>Total Workouts</h2>
                <p>{statistics.totalWorkouts}</p>
            </div>

            <div>
                <h2>Total Volume</h2>
                <p>{statistics.totalVolume} kg</p>
            </div>

            <h2>Personal Records</h2>

            <ul>
                {statistics.personalRecords.map((pr) => (
                    <li key={pr.exerciseId}>
                        {pr.exerciseName} - {pr.weight} {pr.unit}
                        {" x "}
                        {pr.reps}
                        {" | Estimated 1RM: "}
                        {pr.estimatedOneRepMax} kg
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default Dashboard;