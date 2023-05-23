import { useState, useEffect } from "react";
import api from "../api";
import UserTable from "../components/UserTable";

const User = () => {
    const [users, setUsers] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalUsers, setTotalUsers] = useState(0);
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        async function getUsersList() {
          setLoading(true);
          try{
            const response = await api.get(`/User?page=2`);
            setUsers(response.data.users);
            setTotalPages(response.data.totalPages);
            setTotalUsers(response.data.totalUsers);
            setCurrentPage(response.data.currentPage);
            setLoading(false);
          } catch({err}) {
            console.log(err);
            setLoading(false)
          }
        }
        getUsersList();
      }, [api]);

    const pagesHandler = () => {
        const result = [];
        console.log('TOTAL PAGES: ' + totalPages)
        console.log('TOTAL USERS: ' + totalUsers)
        console.log('CURRENT PAGE: ' + currentPage)
        for (let index = 0; index < totalPages; index++) {
            result.push(index)
        }
        return result;
    }
    if(loading)
      return <div><h1>Loading...</h1></div>

    return(
        <div>
            <h2>Lista de Usuários</h2>
            <UserTable users={users}/>
            <div>
                {pagesHandler().map(c => c+1)}
            </div>
        </div>
    )
}

export default User;