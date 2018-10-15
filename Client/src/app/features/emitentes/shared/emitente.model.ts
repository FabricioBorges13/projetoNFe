import { Endereco } from './../../endereco/endereco.model';

export class Emitente {
    public id: number;
    public inscricaoMunicipal: string;
    public nomeRazaoSocial: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public endereco: Endereco;
}

export class EmitenteDataCommand {
    public id?: number;
    public nomeRazaoSocial: string;
    public inscricaoMunicipal: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public endereco: Endereco;

    constructor(emitente: any) {
        this.id = emitente.id;
        this.inscricaoMunicipal = emitente.inscricaoMunicipal;
        this.inscricaoEstadual = emitente.inscricaoEstadual;
        this.nomeRazaoSocial = emitente.nomeRazaoSocial;
        this.numeroDoDocumento = emitente.numeroDoDocumento;
        this.endereco = new Endereco();
        this.endereco.logradouro = emitente.logradouro;
        this.endereco.bairro = emitente.bairro;
        this.endereco.numero = emitente.numero;
        this.endereco.municipio = emitente.municipio;
        this.endereco.estado = emitente.estado;
        this.endereco.pais = 'Brasil';
    }
}

export class EmitenteDeleteCommand {
    public emitenteIds: number[] = [];

    constructor(emitentes: Emitente[]) {
        this.emitenteIds = emitentes.map((e: Emitente) => e.id);
    }
}

export class EmitenteFormModel {
    public nomeRazaoSocial: string;
    public inscricaoMunicipal: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public endereco: any;

    constructor(emitente: any) {
        this.inscricaoMunicipal = emitente.inscricaoMunicipal;
        this.inscricaoEstadual = emitente.inscricaoEstadual;
        this.nomeRazaoSocial = emitente.nomeRazaoSocial;
        this.numeroDoDocumento = emitente.numeroDoDocumento;
        this.endereco = new Endereco();
        this.endereco.logradouro = emitente.logradouro;
        this.endereco.bairro = emitente.bairro;
        this.endereco.numero = emitente.numero;
        this.endereco.municipio = emitente.municipio;
        this.endereco.estado = emitente.estado;
    }
}
