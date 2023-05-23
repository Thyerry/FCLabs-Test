import { Outlet } from "react-router-dom"

const Home = () => {
    return (
        <div className='App'>
            <h1>FCxLabs</h1>
            <Outlet />
        </div>
    )
}

export default Home;