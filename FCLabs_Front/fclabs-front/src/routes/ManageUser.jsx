import { useState } from 'react';
import { useLocation } from 'react-router-dom' 
import api from '../api';
import './ManageUser.scss'
import ManageUserInput from '../components/ManageUserInput'

const ManageUser = () => {
    const [id, setId] = useState('');
    const [name, setName] = useState('');
    const [login, setLogin] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [cpf, setCpf] = useState('');
    const [birthdate, setBirthdate] = useState('');
    const [motherName, setMotherName] = useState('');
    const [error, setError] = useState('');

    console.log("error " + error)
    
    const handleSubmit = async () => {
        if(id === '')
            await handleAddUser({
                name,
                login,
                email,
                phone,
                cpf,
                birthdate,
                motherName
            });
    };

    const handleAddUser = async (requestBody) => {
        try {
            const response = await api.post('/User', requestBody);

        } catch ({response}) {
            console.log(response.data.title)
            setError(response.data.title)
        }
    }  
    return (
        <div className="user-form">
            <h1>Adicionar Usuário</h1>
            <div className="form-container">
                <ManageUserInput 
                    type="text"
                    title="Nome"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <ManageUserInput 
                    type="text"
                    title="Login"
                    value={login}
                    onChange={(e) => setLogin(e.target.value)}
                />
                <ManageUserInput 
                    type="text"
                    title="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <ManageUserInput 
                    type="tel"
                    title="Telefone"
                    value={phone}
                    onChange={(e) => setPhone(e.target.value)}
                />
                <ManageUserInput 
                    type="text"
                    title="CPF"
                    value={cpf}
                    onChange={(e) => setCpf(e.target.value)}
                />
                <ManageUserInput 
                    type="date"
                    title="Data de Nascimento"
                    value={birthdate}
                    onChange={(e) => setBirthdate(e.target.value)}
                />
                <ManageUserInput 
                    type="text"
                    title="Nome da Mãe"
                    value={motherName}
                    onChange={(e) => setMotherName(e.target.value)}
                />
                <button onClick={handleSubmit}>Salvar</button>
            </div>
            {
                !error && 
                <div>
                    Erro: {error}
                </div>
            }
        </div>
    );
}

export default ManageUser;