import { Endereco } from './../../endereco/endereco.model';
export class Destinatario {
    public id: number;
    public nomeRazaoSocial: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public endereco: Endereco;
}

export class DestinatarioAddCommand {
    public nomeRazaoSocial: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public endereco: Endereco;

    constructor(destinatario: any) {
        this.nomeRazaoSocial = destinatario.nomeRazaoSocial;
        this.inscricaoEstadual = destinatario.inscricaoEstadual;
        this.numeroDoDocumento = destinatario.numeroDoDocumento;
        this.endereco = new Endereco();
        this.endereco.logradouro = destinatario.logradouro;
        this.endereco.bairro = destinatario.bairro;
        this.endereco.numero = destinatario.numero;
        this.endereco.municipio = destinatario.municipio;
        this.endereco.estado = destinatario.estado;
        this.endereco.pais = 'Brasil';
    }
}

export class DestinatarioUpdateCommand {
    public id: number;
    public nomeRazaoSocial: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public endereco: Endereco;

    constructor(destinatario: Destinatario) {
        this.id = destinatario.id;
        this.nomeRazaoSocial = destinatario.nomeRazaoSocial;
        this.inscricaoEstadual = destinatario.inscricaoEstadual;
        this.numeroDoDocumento = destinatario.numeroDoDocumento;
        this.endereco = destinatario.endereco;
    }
}

export class DestinatarioDeleteCommand {
    public destinatarioIds: number[];

    constructor(products: Destinatario[]) {
        this.destinatarioIds = products.map((p: Destinatario) => p.id);
    }
}
