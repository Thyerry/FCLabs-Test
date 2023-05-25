export const mountListQuery = ({
    name,
    status,
    cpf,
    login,
    birthDateRangeStart,
    birthDateRangeEnd,
    lastChangeDateRangeStart,
    lastChangeDateRangeEnd,
    inclusionDateRangeStart,
    inclusionDateRangeEnd,
    ageRange
}) => {
    let query = '';

    if(name !== '') query += `&name=${name}`;
    if(cpf !== '') query += `&cpf=${cpf}`;
    if(login !== '') query += `&login=${login}`;
    if(birthDateRangeStart !== '') query += `&birthDateRangeStart=${birthDateRangeStart}`;
    if(birthDateRangeEnd !== '') query += `&birthDateRangeEnd=${birthDateRangeEnd}`;
    if(lastChangeDateRangeStart !== '') query += `&lastChangeDateRangeStart=${lastChangeDateRangeStart}`;
    if(lastChangeDateRangeEnd !== '') query += `&lastChangeDateRangeEnd=${lastChangeDateRangeEnd}`;
    if(inclusionDateRangeStart !== '') query += `&inclusionDateRangeStart=${inclusionDateRangeStart}`;
    if(inclusionDateRangeEnd !== '') query += `&inclusionDateRangeEnd=${inclusionDateRangeEnd}`;
    if(ageRange !== '') query += `&ageRange=${ageRange}`;
    
    if(status.length == 0) query += `&returnActive=false&returnInactive=false`;
    if(status.length == 1){
        if(status[0] == '1')
            query += `&returnActive=true&returnInactive=false`;
        if(status[0] == '2')
            query += `&returnActive=false&returnInactive=true`;
    }
    if(status.length == 2) query += `&returnActive=true&returnInactive=true`;
    console.log(`Mount Query ${status.length}`)
    return query;
}