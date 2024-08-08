import { useState } from 'react';
import './App.css';
import './index.css';

interface Response<T> {
	error: string;
	data: T;
	success: boolean;
}

function App() {
	const [distance, setDistance] = useState<number>(0);
	const [iata1, setIata1] = useState<string>('AMS');
	const [iata2, setIata2] = useState<string>('SVO');
	const [error, setError] = useState<string>();

	const contents = <strong><p>Distance: {distance} miles</p></strong>;

	return (
		<div>
			<h1>Airports Distance</h1>
			<p>This component demonstrates fetching data from the server.</p>
			<div>
				<button
					className="button"
					type="button"
					onClick={async () => {
						await getDistance();
					}}
				>
					Get Distance
				</button>
			</div>
			{contents}
		</div>
	);

	async function getDistance() {
		const fetchResponse = await fetch(`distance?iata1=${iata1}&iata2=${iata2}`);
		const response = await fetchResponse.json() as Response<number>;

		if (!response.success) {
			setError(response.error ?? 'Something went wrong...');
		} else {
			setDistance(response.data);
		}
	}
}

export default App;