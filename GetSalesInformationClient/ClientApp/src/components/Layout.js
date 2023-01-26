import React from 'react';
import ProductSalesTable from './SalesNumber';
import OrderTimesTable from './OrderTimes';
import Timer from './Timer';
import GetMessageByTimer from './GetMessageByTimer.js';


function Layout() {
    

    return (
        <div>
         <Timer/>
        <div className="tableDiv">
                <div className="table_caption"><label>Max number of Sales of products in city</label></div>
                <ProductSalesTable />
                <GetMessageByTimer />
        </div>
            <div className="tableDiv">
                <div className="table_caption"><label>Order Arriving Before/After required time</label></div>
            <OrderTimesTable />
            </div>
            
        </div>
    );

}

 

export default Layout;
