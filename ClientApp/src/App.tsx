import { useState } from 'react';

function App() {
    const [message, setMessage] = useState('');

    const createWordDocument = async () => {
        console.log('Button was clicked!');
        try {
            const response = await fetch(
                'https://localhost:7186/Home/TestWordDocument'
            );

            const text = await response.text();

            setMessage(text);
        }
        catch (error) {
            setMessage('Error connecting to the server.');
            console.error(error);
        }
    };

    return (
        <div>
        <h1>React App </h1>

            < button onClick = { createWordDocument } >
                Create Word Document
                    </button>

                    < p > { message } </p>
                    </div>
    );
}

export default App;