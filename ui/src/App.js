import "./App.css";
import config from "./config.json";
import { useState } from "react";

function App() {
  const [localSalesCount, setLocalSalesCount] = useState("");
  const [foreignSalesCount, setForeignSalesCount] = useState("");
  const [averageSaleAmount, setAverageSaleAmount] = useState("");

  const [totalFcamaraCommission, setTotalFcamaraCommission] = useState(50);
  const [totalCompetitorCommission, setTotalCompetitorCommission] = useState(10);


  function calculate() {
    console.log(JSON.stringify({
      localSaleCount: localSalesCount,
      foreignSaleCount: foreignSalesCount,
      avgSaleAmt: averageSaleAmount,
    }))
    fetch(config.apiUrl, {
      method: "POST",
      headers: {
        Accept: "application/json, text/plain, */*",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        localSaleCount: localSalesCount,
        foreignSaleCount: foreignSalesCount,
        avgSaleAmt: averageSaleAmount,
      }),
    })
      .then((res) => res.json())
      .then((res) => {setTotalFcamaraCommission(res.fcamaraCommission);
      setTotalCompetitorCommission(res.competitorCommission)});

  }
  return (
    <div className="App">
      <header className="App-header">
        <div></div>
        <form onSubmit={calculate}>
          <label htmlFor="localSalesCount">Local Sales Count</label>
          <input
            type="number"
            id="localSalesCount"
            value={localSalesCount}
            onChange={(e) => setLocalSalesCount(e.target.value)}
          /><br />

          <label htmlFor="foreignSalesCount">Foreign Sales Count</label>
          <input
            type="number"
            id="foreignSalesCount"
            value={foreignSalesCount}
            onChange={(e) => setForeignSalesCount(e.target.value)}
          /><br />

          <label htmlFor="averageSaleAmount">Average Sale Amount</label>
          <input
            type="number"
            id="averageSaleAmount"
            value={averageSaleAmount}
            onChange={(e) => setAverageSaleAmount(e.target.value)}
          /><br />

          <button type="submit">Calculate</button>
        </form>
      </header>

      <div>
        <h3>Results</h3>
        <p>Total FCamara commission: {totalFcamaraCommission}</p>
        <p>Total FCamara commission: {totalCompetitorCommission}</p>
      </div>
    </div>
  );
}

export default App;
