import { GetTemplateDto } from "../../api/dto/GetTemplateDto";
import styles from "./TemplateItem.module.css";
import pdfIcon from "../../assets/icon-pdf.png"

type TemplateItemProps = {
  item: GetTemplateDto;
  onClick:(item:GetTemplateDto) => void;
  onGeneratePdf:(item:GetTemplateDto) => void;
};

function TemplateItem({item,onClick,onGeneratePdf}:TemplateItemProps) {
    
  return (
  <div onClick={()=>onClick(item)} key={item.id} className={styles.item}>
    <div className={styles.item_header}>
      <p>{item.name}</p>
      <img onClick={(e) => {
          onGeneratePdf(item)
          e.stopPropagation();
        }} 
        src={pdfIcon} alt="pdf icon" />
    </div>
      <iframe
        srcDoc={item.content}
        className={styles.html_content}
        title="HTML Preview"/>
  </div>
  );
}

export default TemplateItem
