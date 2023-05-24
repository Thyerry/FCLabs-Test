import { useState, useEffect } from "react";
import api from "../api";
import UserTable from "../components/UserTable";
import Pagination from "../components/Pagination";
import SearchForm from "../components/SearchForm";

const User = () => {
    const [users, setUsers] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalUsers, setTotalUsers] = useState(0);
    const [loading, setLoading] = useState(false)

    const callUserEndpoint = async (page) => {
        const response = await api.get(`/User?page=${page}`);
        setUsers(response.data.users);
        setTotalPages(response.data.totalPages);
        setTotalUsers(response.data.totalUsers);
        setCurrentPage(response.data.currentPage);
    }

    const handleSearch = (queryObject) => {
    }

    useEffect(() => {
        async function getUsersList() {
          setLoading(true);
          try{
            await callUserEndpoint(currentPage)
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
                await callUserEndpoint(currentPage + 1)
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
                await callUserEndpoint(currentPage - 1)
                setLoading(false);
            } catch({err}) {
                console.log(err);
            }
        }
    }

    return(
        <div>
            <h2>Lista de Usuários</h2>
            <div>Essa pesquisa retornou {totalUsers} usuários</div>
            <div className="search-user-container">
                <SearchForm handleSearchCallback={handleSearch}/>
                <div>

                {
                    !loading && 
                    <UserTable 
                        users={users} 
                        edit={() =>  console.log('edição')}
                        inactivate={() => console.log('inativação')}
                    />
                }

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