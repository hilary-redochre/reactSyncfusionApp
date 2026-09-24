import { useState } from 'react';
import { DocumentEditorContainerComponent, Toolbar } from '@syncfusion/ej2-react-documenteditor';
// Syncfusion styles
import '@syncfusion/ej2-base/styles/material.css';
import '@syncfusion/ej2-buttons/styles/material.css';
import '@syncfusion/ej2-inputs/styles/material.css';
import '@syncfusion/ej2-popups/styles/material.css';
import '@syncfusion/ej2-lists/styles/material.css';
import '@syncfusion/ej2-navigations/styles/material.css';
import '@syncfusion/ej2-splitbuttons/styles/material.css';
import '@syncfusion/ej2-dropdowns/styles/material.css';
import '@syncfusion/ej2-documenteditor/styles/material.css';

DocumentEditorContainerComponent.Inject(Toolbar);

function App() {
    //Example 2

    const [message, setMessage] = useState(''); const createInvoice = async () => {
        try
        {
            const response = await fetch('https://localhost:7186/api/documents/invoice/123');
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
        <div style= {{ height: '100vh' }}>

            <h1>Sales Invoice</h1>
            <button onClick = { createInvoice }>
                Create Sales Invoice
            </button>
            <p> { message } </p>

            <DocumentEditorContainerComponent id= "container" height= "100%" enableToolbar={true}/>
        </div>
    );


    
}

export default App;
