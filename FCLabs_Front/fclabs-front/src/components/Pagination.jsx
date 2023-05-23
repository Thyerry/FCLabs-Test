/* eslint-disable react/prop-types */
const Pagination = ({
    currentPage,
    totalPages,
    canNextPage,
    canPreviousPage,
    previousPage,
    nextPage
}) => {
    return(
        <div className="pagination">
            <button onClick={previousPage} disabled={!canPreviousPage}>
                Anterior
            </button>
            <span>
                Página{' '}
            <strong>
                {currentPage} de {totalPages}
            </strong>
            </span>
            <button onClick={nextPage} disabled={!canNextPage}>
                Próxima
            </button>
      </div>
    )
}
 
export default Pagination;