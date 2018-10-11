export class Produto {
    public id: number;
    public codigoProduto: string;
    public descricao: string;
    public quantidade: number;
    public valorUnitario: number;
    public valorIpi: number;
    public valorIcms: number;
}

export class ProdutoUpdateCommand{
    public id: number;
    public codigoProduto: string;
    public descricao: string;
    public quantidade: number;
    public valorUnitario: number;
    public valorIpi: number;
    public valorIcms: number;

    constructor(produto: Produto) {
        this.id = produto.id;
        this.codigoProduto = produto.codigoProduto;
        this.descricao = produto.descricao;
        this.quantidade = produto.quantidade;
        this.valorUnitario = produto.valorUnitario;
        this.valorIpi = produto.valorIpi;
        this.valorIcms = produto.valorIcms;
    }
}

export class ProdutoDataCommand {
    public codigoProduto: string;
    public descricao: string;
    public quantidade: number;
    public valorUnitario: number;
    public valorIpi: number;
    public valorIcms: number;

    constructor(produto: Produto) {
        this.codigoProduto = produto.codigoProduto;
        this.descricao = produto.descricao;
        this.quantidade = produto.quantidade;
        this.valorUnitario = produto.valorUnitario;
        this.valorIpi = produto.valorIpi;
        this.valorIcms = produto.valorIcms;
    }
}

export class ProdutoDeleteCommand {
    public produtoIds: number[] = [];

    constructor(produtos: Produto[]) {
        this.produtoIds = produtos.map((e: Produto) => e.id);
    }
}
