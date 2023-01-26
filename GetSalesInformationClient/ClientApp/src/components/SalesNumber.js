import React, { useState, useEffect } from 'react';

function ProductSalesTable() {
    
    const [data, setData] = useState([]);
    const [sortedData, setSortedData] = useState([]);
    const [sortBy, setSortBy] = useState('numsales');
    const [sortDirection, setSortDirection] = useState('desc');

    const handleSort = column => {
        if (sortBy === column) {
            setSortDirection(sortDirection === 'asc' ? 'desc' : 'asc');
        } else {
            setSortBy(column);
            setSortDirection('asc');
        }

        setSortedData([...data].sort((a, b) => {
            if (sortDirection === 'asc') {
                return a[column] > b[column] ? 1 : -1;
            } else {
                return a[column] < b[column] ? 1 : -1;
            }
        }));
    }

    useEffect(() => {
        fetch('sales/GetProductSales?orderBy=NumSales&orderDir=desc')
            .then(response => response.json())
            .then(data => {
                setData(data);
                setSortedData(data);
            })
            .catch(error => console.error(error));
    }, []);

    if (sortedData.length === 0) {
        return <div>Loading...</div>;
    }

    return (
        <table>
            <thead>
                <tr>
                    <th className="sortedTh" onClick={() => handleSort('productName')}>Product Name</th>
                    <th className="sortedTh" onClick={() => handleSort('city')}>City</th>
                    <th className="sortedTh" onClick={() => handleSort('numOfSales')}>Number of Sales</th>
                </tr>
            </thead>
            
            <tbody>
                {sortedData.map((item,index) => (
                    <tr key={index}>
                        <td className="pname">{item.productName}</td>
                        <td>{item.city}</td>
                        <td>{item.numOfSales}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default ProductSalesTable;