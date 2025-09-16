import React, { useEffect, useState } from 'react';
import './App.css';
import TemplatesApi from './api/TemplatesApi';
import { GetTemplateDto } from './api/dto/GetTemplateDto';
import TemplateItem from './components/TemplateItem/TemplateItem';
import CreateTemplatePopup from './components/CreateTemplatePopup/CreateTemplatePopup';
import MainTemplatePopup from './components/MainTemplatePopup/MainTemplatePopup';
import GeneratePdfPopup from './components/GeneratePdfPopup/GeneratePdfPopup';

function App() {
  
const templateApi = new TemplatesApi();
const [templates,setTemplates] = useState<GetTemplateDto[] | null>(null)
const [showCreatePopup, setShowCreatePopup] = useState(false);
const [templateToPdf, setTemplateToPdf] = useState<GetTemplateDto | null>(null);
const [currentTemplate, setCurrentTemplate] = useState<GetTemplateDto | null>(null);

const fetchTemplates = async () => {
        try {
            const templates = await templateApi.GetAllTemplates();
            console.log(templates);
            setTemplates(templates);
        } catch (err) {
            console.error("Помилка при отриманні шаблонів:", err);
        }
    };

useEffect(() => {
    fetchTemplates();
}, []);

function openPdfPopup(template:GetTemplateDto){
    setTemplateToPdf(template)
}


function openMainPopup(template:GetTemplateDto){
    setCurrentTemplate(template);
}

async function deleteTemplate(id: string) {
  await templateApi.DeleteTemplate(id);
  setCurrentTemplate(null);
  fetchTemplates();
}

return (
    <div className="App">
        <div style={{
            display: "grid",
            gridTemplateColumns: "repeat(3, 1fr)",
            gap: "20px",
            margin: "25px 40px 0"
        }}>
            {templates?.map(x => (
            <TemplateItem onGeneratePdf={setTemplateToPdf} onClick={openMainPopup} key={x.id} item={x}/>
            ))}

            <button onClick={() => setShowCreatePopup(true)} className="add-template-button">+</button>
            {showCreatePopup && (
                <CreateTemplatePopup
                onClose={() => setShowCreatePopup(false)}
                onCreated={() => fetchTemplates()}

            />
             )}

            {currentTemplate && (
            <MainTemplatePopup
                item={currentTemplate}
                onClose={() => setCurrentTemplate(null)}
                onSaved={() => {
                setCurrentTemplate(null);
                fetchTemplates()
                }}
                onDeleted={deleteTemplate} 
            />
            )}

            {templateToPdf && (
            <GeneratePdfPopup
                item={templateToPdf}
                onClose={() => setTemplateToPdf(null)}
            />
            )}

        </div>

    </div>
  );
}

export default App;
