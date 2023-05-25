import { useState } from 'react';
import DateFilter from './DateFilter';
import './SearchForm.scss'

const SearchForm = ({ handleSearchCallback }) => {
  const [name, setName] = useState('');
  const [status, setStatus] = useState('1');
  const [cpf, setCpf] = useState('');
  const [login, setLogin] = useState('');
  const [birthDateRangeStart, setBirthDateRangeStart] = useState('');
  const [birthDateRangeEnd, setBirthDateRangeEnd] = useState('');
  const [lastChangeDateRangeStart, setLastChangeDateRangeStart] = useState('');
  const [lastChangeDateRangeEnd, setLastChangeDateRangeEnd] = useState('');
  const [inclusionDateRangeStart, setInclusionDateRangeStart] = useState('');
  const [inclusionDateRangeEnd, setInclusionDateRangeEnd] = useState('');
  const [ageRange, setAgeRange] = useState('');

  const handleSearch = () => {
    handleSearchCallback({
      page: 1,
      name,
      status,
      cpf,
      login,
      birthDateRangeStart,
      birthDateRangeEnd,
      lastChangeDateRangeStart,
      lastChangeDateRangeEnd,
      inclusionDateRangeStart,
      inclusionDateRangeEnd,
      ageRange      
    })
  };

  return (
    <div>
      <div className="search-form">
        <input
          className="search-input"
          type="text"
          placeholder="Buscar por nome"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />

        <input
          className="cpf-input"
          type="text"
          placeholder="CPF"
          value={cpf}
          onChange={(e) => setCpf(e.target.value)}
        />

        <input
          className="login-input"
          type="text"
          placeholder="Login"
          value={login}
          onChange={(e) => setLogin(e.target.value)}
        />

        <select className="age-range-select" value={ageRange} onChange={(e) => setAgeRange(e.target.value)}>
          <option value="">Selecione a faixa etária</option>
          <option value="1">Maior que 18 e menor que 26</option>
          <option value="2">Maior que 25 e menor que 31</option>
          <option value="3">Maior que 30 e menor que 36</option>
          <option value="4">Maior que 35 e menor que 41</option>
          <option value="5">Maior que 40</option>
        </select>

      <div className="status-checkboxes">
        <label>
          <input
            type="checkbox"
            checked={status.includes("1")}
            onChange={(e) =>
              setStatus(
                e.target.checked ? [...status, "1"] : status.filter((s) => s !== "1")
              )
            }
          />
          Ativo
        </label>
        <label>
          <input
            type="checkbox"
            checked={status.includes("2")}
            onChange={(e) =>
              setStatus(
                e.target.checked ? [...status, "2"] : status.filter((s) => s !== "2")
              )
            }
          />
          Inativo
        </label>
      </div>
      </div>

      <DateFilter 
        title="Data de nascimento"
        valueFrom={birthDateRangeStart}
        setValueFrom={setBirthDateRangeStart}
        valueTo={birthDateRangeEnd}
        setValueTo={setBirthDateRangeEnd}
      />
      <DateFilter 
        title="Data de Modificação"
        valueFrom={lastChangeDateRangeStart}
        setValueFrom={setLastChangeDateRangeStart}
        valueTo={lastChangeDateRangeEnd}
        setValueTo={setLastChangeDateRangeEnd}
      />
      <DateFilter 
        title="Data de Criação"
        valueFrom={inclusionDateRangeStart}
        setValueFrom={setInclusionDateRangeStart}
        valueTo={inclusionDateRangeEnd}
        setValueTo={setInclusionDateRangeEnd}
      />

      <button onClick={handleSearch}>Buscar</button>
    </div>
  );
};

export default SearchForm;
