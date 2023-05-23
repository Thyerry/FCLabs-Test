import { useState, useEffect } from "react";
import api from "../api";
import UserTable from "../components/UserTable";

const User = () => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        async function getGroceryList() {
          setLoading(true);
          try{
            const response = await api.get(`/User`);
            setUsers(response.data);
            setLoading(false);
          } catch({err}) {
            console.log(err);
            setLoading(false)
          }
        }
        getGroceryList();
      }, [api]);

    if(loading)
      return <div><h1>Loading...</h1></div>

    return(
        <div>
            <h2 style={{ color: 'red' }}>Lista de Usuários</h2>
            <UserTable users={users}/>
        </div>
    )
}

export default User;