import Dashboard from './components/Dashboard';

function App() 
 {
   return <Dashboard />;
}

export default App;

// import {useEffect, useState} from 'react';
// import {type Exercise, getExercises } from './services/api';

// function App()
// {
//   const [exercises, setExercises] = useState<Exercise[]>([]);
//   const [loading, setLoading] = useState(true);
//   const [error, setError] = useState<string | null>(null);

//   useEffect(() => {
//     async function loadExercises(){
//       try {
//         const data = await getExercises();
//         setExercises(data);
//       }
//       catch {
//         setError("Failed to load exercises");
//       }
//       finally {
//         setLoading(false);
//       }
//     }
//     loadExercises();
//   }, []);

//   return(
//     <div>
//       <h1>Gym Tracker</h1>

//       <h2>Exercises</h2>

//       {loading && <p>Loading...</p>}

//       {error && <p>{error}</p>}

//       {!loading && !error &&(
//         <ul>
//           {exercises.map((exercise) => (
//             <li key={exercise.id}>
//               {exercise.name} - {exercise.muscleGroup}
//             </li>
//           ))}
//         </ul>
//       )}
//     </div>
//   )
// }

//export default App;