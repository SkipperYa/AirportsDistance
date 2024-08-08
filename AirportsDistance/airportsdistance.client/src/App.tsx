import { useState } from 'react';
import './App.css';
import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';

interface Response<T> {
	error: string;
	data: T;
	success: boolean;
}

interface DistanceResponse {
	point1: string | undefined;
	point2: string | undefined;
	distance: number;
}

function App() {
	const [distance, setDistance] = useState<DistanceResponse>({ point1: undefined, point2: undefined, distance: 0 });
	const [loading, setLoading] = useState<boolean>(false);
	const [iata1, setIata1] = useState<string>();
	const [iata2, setIata2] = useState<string>();
	const [error, setError] = useState<string>();

	const contents = <strong><h4>Distance {distance.point1 && distance.point2 && `from ${distance.point1} to ${distance.point2}`}: {distance.distance} miles</h4></strong>;

	const examples = [
		'SVO for Moscow Sheremetyevo',
		'EWR for Newark, New Jersey',
		'HVN for New Haven, Connecticut',
		'ORF for Norfolk, Virginia',
		'EYW for Key West, Florida',
		'OME for Nome, Alaska',
		'BNA for Nashville, Tennessee (whose airport\'s original name was Berry Field) ',
		'APC for Napa, California',
		'ILM for Wilmington, North Carolina',
	];

	return (
		<div className="conteiner">
			<h1>Airports Distance</h1>
			<hr />
			<h6>
				REST service to measure distance in miles between two airports. Airports are identified by 3-letter IATA code.&nbsp;
				<br />
				<br />
				<div className="text-start">
					The simplest method of calculating distance relies on some advanced-looking math.&nbsp;
					Known as the Haversine formula, it uses spherical trigonometry to determine the great circle distance between two points.&nbsp;
					<a href="https://en.wikipedia.org/wiki/Haversine_formula">Wikipedia</a> has more on the formulation of this popular straight line distance approximation.
				</div>
				<br />
				<br />
			</h6>
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
						{loading ? <div className="spinner-border" role="status">
							<span className="sr-only" />
						</div> : <>Get Distance</>}
					</button>
				</div>
			</div>
			<br />
			<div className="text-center">
				<p>{contents}</p>
				{error && <div className="text-danger">
					<hr />
					<h4>Error: {error}</h4>
				</div>}
			</div>
			<div className="text-start">
				For example:
				<br />
				<ul>
					{examples.map((example, key) => {
						return <li key={key}>
							{example}
						</li>
					})}
				</ul>
			</div>
			<br />
		</div>
	);

	async function getDistance() {
		setLoading(true);
		const fetchResponse = await fetch(`distance?iata1=${iata1}&iata2=${iata2}`);
		const response = await fetchResponse.json() as Response<DistanceResponse>;

		if (!response.success) {
			setError(response.error ?? 'Something went wrong...');
		} else {
			setError(undefined);
			setDistance(response.data);
		}

		setLoading(false);
	}
}

export default App;