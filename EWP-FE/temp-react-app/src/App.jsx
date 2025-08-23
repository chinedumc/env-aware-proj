import { useEffect, useState } from "react";
import { loadingConfig } from "./configLoader"; // Assume you have a configLoader.js file
import axios from "axios";

const App = () => {
	const [accounts, setAccounts] = useState([]);
	const config = loadingConfig(); // Assume loadingConfig is a function that loads your config

	useEffect(() => {
		axios.get(`${config.api.baseUrl}/accounts`).then((res) => {
			setAccounts(res.data);
		});
	}, []);
	return (
		<div>
			<h1>Accounts ({import.meta.env.VITE_APP_ENV})</h1>
			<ul>
				{accounts.map((acc) => (
					<li key={acc.id}>{acc.name}</li>
				))}
			</ul>
		</div>
	);
};

export default App;
