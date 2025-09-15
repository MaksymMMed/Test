import axios from "axios";

class TemplatesApi {
    apiPath: string = "http://localhost:3001/API";

    public async CreateTemplate(dto: CreateTemplateDto): Promise<boolean> {
        try {
            const response = await axios.post<boolean>(`${this.apiPath}/template`, dto);
            return response.data;
        } catch (err) {
            throw err;
        }
    }

    public async UpdateTemplate(dto: UpdateTemplateDto): Promise<boolean> {
        try {
            const response = await axios.put<boolean>(`${this.apiPath}/template`, dto);
            return response.data;
        } catch (err) {
            throw err;
        }
    }

    public async DeleteTemplate(id: string): Promise<boolean> {
        try {
            const response = await axios.delete<boolean>(`${this.apiPath}/template/${id}`);
            return response.data;
        } catch (err) {
            throw err;
        }
    }

    public async GetTemplateById(id: string): Promise<GetTemplateDto> {
        try {
            const response = await axios.get<GetTemplateDto>(`${this.apiPath}/template/${id}`);
            return response.data;
        } catch (err) {
            throw err;
        }
    }

    public async GetAllTemplates(): Promise<GetTemplateDto[]> {
        try {
            const response = await axios.get<GetTemplateDto[]>(`${this.apiPath}/templates`);
            return response.data;
        } catch (err) {
            throw err;
        }
    }

public async GeneratePdf(dto: GeneratePdfDto): Promise<void> {
    try {
        const response = await axios.post(`${this.apiPath}/template/pdf`, dto, {
            responseType: "blob"
        });

        const url = window.URL.createObjectURL(response.data);
        const link = document.createElement("a");
        link.href = url;
        link.download = "file.pdf";
        document.body.appendChild(link);
        link.click();
        link.remove();

        window.URL.revokeObjectURL(url);
    } catch (err) {
        throw err
    }
}
}
