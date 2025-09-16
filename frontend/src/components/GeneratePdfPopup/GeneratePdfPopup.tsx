import React, { useState } from "react";
import TemplatesApi from "../../api/TemplatesApi";
import styles from "./GeneratePdfPopup.module.css";
import { GetTemplateDto } from "../../api/dto/GetTemplateDto";
import { GeneratePdfDto } from "../../api/dto/GeneratePdfDto";

type MainTemplatePopupProps = {
  onClose: () => void;
  item: GetTemplateDto;
};

export default function MainTemplatePopup({ onClose, item }: MainTemplatePopupProps) {
  const templateApi = new TemplatesApi();
  const [placeholderValues, setPlaceholderValues] = useState<Record<string, string>>(
    item.placeholders.reduce((acc, key) => ({ ...acc, [key]: "" }), {})
  );

  const handlePdfGeneration = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const dto: GeneratePdfDto = {
        templateId: item.id,
        placeholderValues
      };
      await templateApi.GeneratePdf(dto); // має бути з responseType: "blob"
      onClose();
    } catch (err) {
      console.error(err);
      alert("Помилка при генерації PDF");
    }
  };

  return (
    <div className={styles.overlay}>
      <div className={styles.popup}>
        <h2>Генерація PDF</h2>
        <form onSubmit={handlePdfGeneration}>
          <div className={styles.placeholders}>
            <h3>Введіть значення</h3>
            {item.placeholders.map((key) => (
              <label key={key} style={{ display: "block", marginBottom: "5px" }}>
                {key}:
                <input
                  type="text"
                  value={placeholderValues[key] || ""}
                  onChange={(e) =>
                    setPlaceholderValues((prev) => ({ ...prev, [key]: e.target.value }))
                  }
                />
              </label>
            ))}
          </div>
          <div className={styles.buttons_placeholder}>
            <button type="submit" className={styles.template_button}>
              Завантажити PDF
            </button>
            <button
              type="button"
              className={styles.template_button}
              onClick={onClose}
            >
              Відмінити
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
