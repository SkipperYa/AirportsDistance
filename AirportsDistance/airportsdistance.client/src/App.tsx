import { useState } from 'react';
import './App.css';
import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';

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
		<div className="conteiner">
			<h1>Airports Distance</h1>
			<hr />
			<p>REST service to measure distance in miles between two airports.</p>
			<br />
			<div className="row">
				<div className="col-sm">
					<div className="input-group mb-3">
						<div className="input-group-prepend">
							<span className="input-group-text" id="inputGroup-sizing-default">IATA 1</span>
						</div>
						<input
							type="text"
							className="form-control"
							onChange={(e) => {
								setIata1(e.target.value);
							}}
						/>
					</div>
				</div>
				<div className="col-sm">
					<div className="input-group mb-3">
						<div className="input-group-prepend">
							<span className="input-group-text" id="inputGroup-sizing-default">IATA 2</span>
						</div>
						<input
							type="text"
							className="form-control"
							onChange={(e) => {
								setIata2(e.target.value);
							}}
						/>
					</div>
				</div>
				<div className="col-sm">
					<button
						className="btn btn-dark"
						type="button"
						onClick={async () => {
							await getDistance();
						}}
					>
						Get Distance
					</button>
				</div>
			</div>
			<br />
			<div className="text-center">
				{contents}
			</div>
			{error && <div className="text-danger">
				{error}
			</div>}
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