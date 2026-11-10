const  API_URL = import.meta.env.VITE_API_URL ?? "http://localhost:5163/api";

export interface Exercise
{
    id: number;
    name: string;
    muscleGroup: string;
}

export async function getExercises(): Promise<Exercise[]>
{
    const response = await fetch(`${API_URL}/exercises`);

    if(!response.ok)
    {
        throw new Error("Failed to fetch exercises");
    }

    return response.json();
}

export interface PersonalRecord
{
    exerciseId: number;
    exerciseName: string;
    weight: number;
    unit: "Kilograms" | "Pounds";
    reps: number;
    estimatedOneRepMax: number;
}

export interface Statistics
{
    totalWorkouts: number;
    totalVolume: number;
    personalRecords: PersonalRecord[];
}

export async function getStatistics(): Promise<Statistics>
{
    const response = await fetch(`${API_URL}/statistics`);

    if (!response.ok)
        {
            throw new Error("Failed to fetch statistics");
        }

        return response.json();
}

export interface ExerciseProgress 
{
    date: string;
    estimatedOneRepMax: number;
}

export interface ExerciseStatistics 
{
    exerciseId: number;
    exerciseName: string;
    bestWeight: number;
    bestWeightUnit: "Kilograms" | "Pounds";
    bestWeightReps: number;
    bestEstimatedOneRepMax: number;
    totalVolume: number;
    progress: ExerciseProgress[];
}

export async function getExerciseStatistics(exerciseId: number): Promise<ExerciseStatistics> 
{
    const response = await fetch(`${API_URL}/statistics/exercises/${exerciseId}`);

    if (!response.ok)
        {
            throw new Error("Failed to fetch exercise statistics");
        }
    return response.json();

}