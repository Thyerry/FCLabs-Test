import './App.css'

import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Home from './routes/Home.jsx'
import User from './routes/User.jsx'
import ManageUser from './routes/ManageUser.jsx'
import ErrorPage from './routes/ErrorPage.jsx'
import Login from './routes/Login'
import Register from './routes/Register'

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
    errorElement: <ErrorPage />,
    children : [
      {
        path: '/login',
        element: <Login />
      },
      {
        path: '/register',
        element: <Register />
      },
      {
        path: '/users',
        element: <User />
      },
      {
        path: '/manage-user' ,
        element: <ManageUser />
      },
    ]
  }
]);

function App() {
  return (
    <RouterProvider router={router} />
  )
}

export default App
