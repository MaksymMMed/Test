import React, { useState } from "react";
import { CreateTemplateDto } from "../../api/dto/CreateTemplateDto";
import TemplatesApi from "../../api/TemplatesApi";
import styles from "./CreateTemplatePopup.module.css";

type TemplatePopupProps = {
  onClose: () => void;
  onCreated: () => void;
};



export default function CreateTemplatePopup({ onClose, onCreated }: TemplatePopupProps) {
  const htmlTemplate:string =
  `<!DOCTYPE html>
  <html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Document</title>
  </head>
  <body>
    Для створення плейсхолдерів використовувати **placeholder name **
  </body>
  </html>`

  const [name, setName] = useState<string>("");
  const [content, setContent] = useState<string>(htmlTemplate);

  const templateApi = new TemplatesApi();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const dto: CreateTemplateDto = {
      name,
      content
    };

    const success = await templateApi.CreateTemplate(dto);
    if (success) {
      onCreated();
      onClose();
    } else {
      alert("Помилка при створенні шаблону");
    }
  };

  return (
    <div className={styles.overlay}>
      <div className={styles.popup}>
        <h2>Новий шаблон</h2>
        <form onSubmit={handleSubmit}>
          <label>
            Назва:
            <input value={name} onChange={e => setName(e.target.value)} required />
          </label>
          <label>
            Контент HTML:
            <textarea style={{height:"300px"}} value={content} onChange={e => setContent(e.target.value)} required />
          </label>
          <div className={styles.buttons_placeholder}>
            <button className={styles.template_button} type="submit">Створити</button>
            <button className={styles.template_button} type="button" onClick={onClose}>Відмінити</button>
          </div>
        </form>
      </div>
    </div>
  );
}
