/* eslint-disable react/jsx-key */
import { useMemo } from 'react';
import { useTable } from 'react-table';
import { Link } from 'react-router-dom'

import './UserTable.scss'

const UserTable = ( { users }) => {
  const data = useMemo(
    () => users,
    []
  );

  const columns = useMemo(
    () => [
      { Header: 'Nome', accessor: 'name',},
      { Header: 'CPF', accessor: 'cpf' },
      { Header: 'Email', accessor: 'email' },
      { Header: 'Telefone', accessor: 'phone' },
      { Header: 'Login', accessor: 'login', },
      { 
        Header: 'Situação', 
        accessor: 'status', 
        Cell:({value}) => {
          return(
            <label>
              {value == 1 ? 'Ativo': value == 2 ? 'Inativo' : 'Bloqueado'}
            </label>
          )
        }},
      { 
        Header: 'Nascimento', 
        accessor: 'birthDate',
        Cell: ({value}) => {
          return (<label>
            {value.slice(0, 10).split('-').reverse().join('/')}
          </label>)
        }
      },
      { 
        Header: 'Adicionado', 
        accessor: 'inclusionDate', 
        Cell: ({value}) => {
          return (
            <label>
              {value.slice(0, 10).split('-').reverse().join('/')}
            </label>
          )
        }
      },
      { 
        Header: 'Ultima Alteração',
        accessor: 'lastChangeDate',
        Cell: ({value}) => {
          return (
            <label>
              {value.slice(0, 10).split('-').reverse().join('/')}
            </label>
          )
        }
      },
      {
        Header: 'Ações',
        Cell: ({cell}) => (
          <div>
            <Link to={{
              pathname: "../manage-user",
              state : {
                user: cell.row.original,
              }
            }}>  
              <button className="action-button-edit">✍</button>
            </Link>
            <button 
              className="action-button-delete"
              onClick={() => console.log(cell.row.original.id)}
              >
                ❌
            </button>
          </div>
        ),
      },
    ],
    []
  );

  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    rows,
    prepareRow,
  } = useTable({ columns, data });

  return (
    <div className="user-list-container">
      <table {...getTableProps()} className="user-table">
        <thead>
          {headerGroups.map((headerGroup) => (
            <tr {...headerGroup.getHeaderGroupProps()}>
              {headerGroup.headers.map((column) => (
                <th {...column.getHeaderProps()}>{column.render('Header')}</th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody {...getTableBodyProps()}>
          {rows.map((row) => {
            prepareRow(row);
            return (
              <tr {...row.getRowProps()}>
                {row.cells.map((cell) => (
                  <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
                ))}
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

export default UserTable;
