import { useEffect, useState } from 'react';
import './App.css';

function App() {
	const [distance, setDistance] = useState<number>(0);

	useEffect(() => {
		getDistance();
	}, []);

	const contents = <strong><p>Distance: {distance}</p></strong>;

	return (
		<div>
			<h1 id="tableLabel">Airports Distance</h1>
			<p>This component demonstrates fetching data from the server.</p>
			{contents}
		</div>
	);

	async function getDistance() {
		const response = await fetch('distance');
		const data = await response.json();
		setDistance(data);
	}
}

export default App;