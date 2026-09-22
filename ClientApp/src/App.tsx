import { useState } from 'react';

function App() {
    //Example 2

    const [message, setMessage] = useState(''); const createInvoice = async () => {
        try
        {
            const response = await fetch('https://localhost:7186/api/Word/SalesInvoice?id=1&SaveOption=WordDoc&Button=Create');
            if (!response.ok)
            {
                throw new Error(`HTTP error: ${response.status}`);
            }
            // The controller returns a Word file. 
            const blob = await response.blob();
            // Create a temporary download link. 
            const url = window.URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = url;
            link.download = 'SalesInvoice.doc';
            document.body.appendChild(link);
            link.click(); link.remove();
            window.URL.revokeObjectURL(url);
            setMessage('Sales invoice created.');
        }
        catch (error)
        {
            console.error(error); setMessage('Error connecting to WordController.');
        }
    };
    return (
        <div>
            <h1>Sales Invoice</h1>
            <button onClick = { createInvoice }>
                Create Sales Invoice
            </button>
            <p> { message } </p>
        </div>
    );




    {/* Example 1 */}
     {/* const [message, setMessage] = useState('');

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
     ); */}
}

export default App;
