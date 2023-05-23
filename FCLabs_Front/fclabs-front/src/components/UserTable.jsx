/* eslint-disable react/jsx-key */
import { useMemo } from 'react';
import { useTable } from 'react-table';

import './UserTable.scss'

const UserTable = ( { users }) => {
  const data = useMemo(
    () => users,
    []
  );

  const columns = useMemo(
    () => [
      { Header: 'Nome', accessor: 'name' },
      { Header: 'CPF', accessor: 'cpf' },
      { Header: 'Email', accessor: 'email' },
      { Header: 'Telefone', accessor: 'phone' },
      { Header: 'Login', accessor: 'login', },
      { Header: 'Situação', accessor: 'status', },
      { Header: 'Nascimento', accessor: 'birthDate', },
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
