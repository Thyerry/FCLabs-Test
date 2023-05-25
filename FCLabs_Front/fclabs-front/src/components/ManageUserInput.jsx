const ManageUserInput = ({title, type, value, onChange}) => {
    return (
        <div>
            <label>{title}:</label>
            <input type={type} value={value} onChange={onChange} />
        </div>
    )
}

export default ManageUserInput;