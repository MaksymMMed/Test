import React, { useState } from "react";
import TemplatesApi from "../../api/TemplatesApi";
import styles from "./MainTemplatePopup.module.css";
import { GetTemplateDto } from "../../api/dto/GetTemplateDto";
import { UpdateTemplateDto } from "../../api/dto/UpdateTemplateDto";

type MainTemplatePopupProps = {
  onClose: () => void;
  onSaved: () => void;
  onDeleted: (id:string) => void;
  item:GetTemplateDto
};

export default function MainTemplatePopup({ onClose, onSaved,onDeleted,item }: MainTemplatePopupProps) {

  const id = item.id
  const [name, setName] = useState<string>(item.name);
  const [content, setContent] = useState<string>(item.content);
  const [placeholders, setPlaceholders] = useState<string[]>(item.placeholders);

  const templateApi = new TemplatesApi();

  const handleDelete = async() => {
    await templateApi.DeleteTemplate(item.id)
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const dto: UpdateTemplateDto = {
        id,
        name,
        content
    };

    const success = await templateApi.UpdateTemplate(dto);
    if (success) {
      onSaved();
      onClose();
    } else {
      alert("Помилка при оновленні шаблону");
    }
  };

  return (
    <div className={styles.overlay}>
      <div className={styles.popup}>
        <h2>Шаблон</h2>
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
            <button className={styles.template_button} style={{backgroundColor:"red"}} onClick={() => onDeleted(id)} type="button">Видалити</button>
            <button className={styles.template_button} type="submit">Оновити</button>
            <button className={styles.template_button} type="button" onClick={onClose}>Відмінити</button>
          </div>
        </form>
      </div>
    </div>
  );
}
