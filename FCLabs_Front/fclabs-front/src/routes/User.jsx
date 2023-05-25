import { useState, useEffect } from "react";
import api from "../api";
import UserTable from "../components/UserTable";
import Pagination from "../components/Pagination";
import SearchForm from "../components/SearchForm";
import * as apiUtils from "../apiUtils"

const User = () => {
    const [users, setUsers] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalUsers, setTotalUsers] = useState(0);
    const [loading, setLoading] = useState(false);
    const [query, setQuery] = useState('');

    const callUserEndpoint = async (page, query) => {
        const response = await api.get(`/User?page=${page}${query}`);
        setUsers(response.data.users);
        setTotalPages(response.data.totalPages);
        setTotalUsers(response.data.totalUsers);
        setCurrentPage(response.data.currentPage);
    }

    const handleSearch = async (queryObject) => {
        const listQuery = apiUtils.mountListQuery(queryObject);
        setQuery(listQuery);
        setCurrentPage(1);
        setLoading(true);
        await callUserEndpoint(currentPage, query);
        setLoading(false);
    }

    useEffect(() => {
        async function getUsersList() {
          setLoading(true);
          try{
            await callUserEndpoint(currentPage, query)
            setLoading(false);
          } catch({err}) {
            console.log(err);
            setLoading(false)
          }
        }
        getUsersList();
      }, [api]);

    const nextPage = async () => {
        if(currentPage < totalPages) {
            setLoading(true);
            try{
                await callUserEndpoint(currentPage + 1, query)
                setLoading(false);
            } catch({err}) {
                console.log(err);
            }
        }
    }

    const previousPage = async () => {
        if(currentPage > 1) {
            setLoading(true);
            try{
                await callUserEndpoint(currentPage - 1, query)
            } catch({err}) {
                console.log(err);
            }
            setLoading(false);
        }
    }
    const tableUpdate = () => {
        return !loading ? (
                <UserTable 
                    users={users} 
                    edit={() => console.log('edit')}
                    inactivate={() => console.log('inativação')}
                />
        ) : <></>
    }
    return(
        <div>
            <h2>Lista de Usuários</h2>
            <div>Essa pesquisa retornou {totalUsers} usuários</div>
            <div className="search-user-container">
                <SearchForm handleSearchCallback={handleSearch}/>
                <div>
                {tableUpdate()}
                <Pagination 
                    currentPage={currentPage}
                    totalPages={totalPages}
                    canNextPage={true}
                    canPreviousPage={true}
                    previousPage={previousPage}
                    nextPage={nextPage}
                />
                </div>
            </div>
        </div>
    )
}

export default User;