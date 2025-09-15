import React, { useEffect, useState } from 'react';
import './App.css';
import TemplatesApi from './api/TemplatesApi';
import { GetTemplateDto } from './api/dto/GetTemplateDto';

function App() {
  
const templateApi = new TemplatesApi();
const [templates,setTemplates] = useState<GetTemplateDto[] | null>(null)

useEffect(() => {
    const fetchTemplates = async () => {
        try {
            const templates = await templateApi.GetAllTemplates();
            console.log(templates);
            setTemplates(templates);
        } catch (err) {
            console.error("Помилка при отриманні шаблонів:", err);
        }
    };

    fetchTemplates();
}, []);

  return (
    <div className="App">
      {templates?.map(x => (
            <div style={{color:"red"}} key={x.id}>
                <p>{x.id}</p>
                <p>{x.name}</p>
                {x.content}
                {x.placeholders}
            </div>
        ))}
    </div>
  );
}

export default App;
