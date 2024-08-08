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
	const [iata1, setIata1] = useState<string>();
	const [iata2, setIata2] = useState<string>();
	const [error, setError] = useState<string>();

	const contents = <strong><h4>Distance: {distance} miles</h4></strong>;

	return (
		<div className="conteiner">
			<h1>Airports Distance</h1>
			<hr />
			<h6>REST service to measure distance in miles between two airports.</h6>
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
						disabled={!iata1 || !iata2}
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
				<p>{contents}</p>
			</div>
			<br />
			{error && <div className="text-danger">
				<hr />
				<h4>Error: {error}</h4>
			</div>}
		</div>
	);

	async function getDistance() {
		const fetchResponse = await fetch(`distance?iata1=${iata1}&iata2=${iata2}`);
		const response = await fetchResponse.json() as Response<number>;

		if (!response.success) {
			setError(response.error ?? 'Something went wrong...');
		} else {
			setError(undefined);
			setDistance(response.data);
		}
	}
}

export default App;