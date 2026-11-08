import 
{
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer
} from "recharts";

interface ProgressChartProps
{
    data: 
    {
        date: string;
        estimatedOneRepMax: number;
    }[];
}

function ProgressChart({ data }: ProgressChartProps) 
{
    const chartData = data.map((point) => 
    ({
        date: new Date(point.date).toLocaleDateString("en-GB"),
        oneRepMax: point.estimatedOneRepMax
    }));

    return (
        <div style={{width: "100%", height: 350}}>
            <ResponsiveContainer>
                <LineChart data={chartData}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date" />
                    <YAxis />
                    <Tooltip />
                    <Line type="monotone" dataKey="oneRepMax" strokeWidth={2} />
                </LineChart>
            </ResponsiveContainer>
        </div>
    )
}

export default ProgressChart;