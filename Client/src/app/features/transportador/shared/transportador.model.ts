import { Endereco } from './../../endereco/endereco.model';
export class Transportador {
    public id: number;
    public nomeRazaoSocial: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public responsabilidadeFrete: boolean;
    public endereco: Endereco;
}

export class TransportadorRemoveCommand {
    public transportadorIds: number[];
}

export class TransportadorAddCommand {
    public nomeRazaoSocial: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public responsabilidadeFrete: boolean;
    public endereco: Endereco;

    constructor(transportador: any) {
        this.nomeRazaoSocial = transportador.nomeRazaoSocial;
        this.numeroDoDocumento = transportador.numeroDoDocumento;
        this.inscricaoEstadual = transportador.inscricaoEstadual;
        this.endereco = new Endereco();
        this.endereco.logradouro = transportador.logradouro;
        this.endereco.bairro = transportador.bairro;
        this.endereco.estado = transportador.estado;
        this.endereco.municipio = transportador.municipio;
        this.endereco.numero = transportador.numero;
        this.endereco.pais = 'Brasil';
    }
}

export class TransportadorUpdateCommand {
    public id: number;
    public nomeRazaoSocial: string;
    public inscricaoEstadual: string;
    public numeroDoDocumento: string;
    public responsabilidadeFrete: boolean;
    public endereco: Endereco;

    constructor(transportador: any) {
        this.id = transportador.id;
        this.nomeRazaoSocial = transportador.nomeRazaoSocial;
        this.numeroDoDocumento = transportador.numeroDoDocumento;
        this.inscricaoEstadual = transportador.inscricaoEstadual;
        this.endereco = new Endereco();
        this.endereco.logradouro = transportador.logradouro;
        this.endereco.bairro = transportador.bairro;
        this.endereco.estado = transportador.estado;
        this.endereco.municipio = transportador.municipio;
        this.endereco.numero = transportador.numero;
        this.endereco.pais = 'Brasil';
    }
}
