import * as signalR from '@microsoft/signalr';
import { useEffect, useState } from 'react';

//listen for a message signal from the server
function GetMessageByTimer() {

    const [connection, setConnection] = useState(null);
    const [message, setMessage] = useState("");

    useEffect(() => {
        const conn = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:44424/timerHub")
            .withAutomaticReconnect()
            .build();

        conn.on("ReceiveMessage", (msg) => {
            setMessage(msg);
        });

        conn.start()
            .then(() => {
                setConnection(conn);
            })
            .catch((err) => {
                console.error(err);
            });
    }, []);

    return (
        <div>
            <p className="messageFromServer">{message}</p>
        </div>
    );
}
export default GetMessageByTimer;
