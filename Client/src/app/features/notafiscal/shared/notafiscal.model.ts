import { Produto } from '../../produto/shared/produto.model';
import { Imposto } from '../../imposto/imposto.model';
import { Emitente } from '../../emitentes/shared/emitente.model';
import { Transportador } from '../../transportador/shared/transportador.model';
import { Destinatario } from '../../destinatario/shared/destinatario.model';

export class NotaFiscal {
    public id: number;
    public naturezaOperacao: string;

    public dataEntrada: Date;

    public produtos: Produto;

    public impostoDaNota: Imposto;

    public emitente: Emitente;

    public transportador: Transportador;

    public destinatario: Destinatario;

    public notaEmitida: boolean;

    public dataEmissao: Date;

    public chaveAcesso: string;

    public valorDoFrete: number;

    public valorTotalDosProdutos: number;

    public valorTotalDaNota: number;
}

export class NotaFiscalDeleteCommand {
    public notafiscalIds: number[] = [];

    constructor(notafiscal: NotaFiscal[]) {
        this.notafiscalIds = notafiscal.map((n: NotaFiscal) => n.id);
    }
}