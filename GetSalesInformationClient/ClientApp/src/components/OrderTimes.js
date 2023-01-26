import React, { useState, useEffect } from 'react';

function OrderTimesTable() {
    const [rowColor, setRowColor] = useState('red');
    const [selectedOption, setSelectedOption] = useState('After');
    const [data, setData] = useState([]);

    const handleOptionChange = (event) => {
        setSelectedOption(event.target.value);
        getData(selectedOption);
        setRowColor(selectedOption === 'after' ? 'red' : 'green');
    };

    useEffect(() => {
        getData('after');
        setRowColor('red');
    }, []);

    function getData(param) {
        fetch('sales/GetOrderTimes?beforeOrAfter=' + param)
            .then(response => response.json())
            .then(data => {
                setData(data);
            })
            .catch(error => console.error(error));
    }

    return (
        <div>
            <div>
                <div>
                    <input type="radio"
                        value="Before"
                        checked={selectedOption === 'Before'}
                        onChange={handleOptionChange}
                    />
                    <label>Orders Shipped Before Arrived Date</label>
                </div>
                <div>
                    <input type="radio"
                        value="After"
                        checked={selectedOption === 'After'}
                        onChange={handleOptionChange}
                    />
                    <label>Orders Shipped After Arrived Date</label>
                </div>
            </div>
            <table className="table-margin">
                <thead>
                    <tr>
                        <th>Order Date</th>
                        <th>Required Date</th>
                        <th>Shipped Date</th>
                        <th>Num of Days { selectedOption } the Required Date</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map(row => (
                        <tr key={row.orderId} style={{ backgroundColor: rowColor }}>
                            <td>{row.orderDate}</td>
                            <td>{row.requiredDate}</td>
                            <td>{row.shippedDate}</td>
                            <td>{row.numOfDiffDays}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
export default OrderTimesTable;