import './DateFilter.scss'

const DateFilter = ({ title, valueFrom, setValueFrom, valueTo, setValueTo }) => {
    return (
      <div className="date-filter-container">
        <div>
          <label className="title">{title}</label>
        </div>
        <div className="date-inputs-container">
          <div className="input-data-container">
            <label className="label">De: </label>
            <input
              className="input-data"
              type="date"
              value={valueFrom}
              onChange={(e) => setValueFrom(e.target.value)}
            />
          </div>
          <div className="input-data-container">
            <label>Fim: </label>
            <input
              className="input-data"
              type="date"
              value={valueTo}
              onChange={(e) => setValueTo(e.target.value)}
            />
          </div>
        </div>
      </div>
    );
  };
  
  export default DateFilter;