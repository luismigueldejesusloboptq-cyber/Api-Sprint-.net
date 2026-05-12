// 1. BLOQUEIO DE SEGURANÇA IMEDIATO
// Esta função roda assim que o arquivo é lido para impedir acesso sem login
const token = localStorage.getItem("token_jwt");

if (!token || token === "undefined" || token === "null") {
    alert("Acesso negado! Por favor, faça login.");
    window.location.href = "login.html";
}

let carrinho = [];

async function carregarProdutos() {
    const container = document.getElementById('lista-produtos');

    // 1. Tenta buscar os dados
    try {
        const response = await fetch('/api/produtos', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        // Se o token estiver vencido ou errado
        if (response.status === 401) {
            alert("Sessão expirada. Faça login novamente.");
            logout();
            return;
        }

        const produtos = await response.json();

        if (produtos.length === 0) {
            container.innerHTML = `<div class="alert alert-info w-100 text-center">Banco de dados vazio.</div>`;
        } else {
            container.innerHTML = produtos.map(p => {
                const idAtual = p.id_produto || p.idProduto;
                return `
                    <div class="col-md-6">
                        <div class="card h-100 produto-card p-3">
                            <div class="card-body text-center">
                                <h5 class="card-title fw-bold">${p.nome}</h5>
                                <p class="text-primary fs-4 fw-bold">R$ ${p.preco.toFixed(2)}</p>
                                <button class="btn btn-dark w-100" onclick="adicionarAoCarrinho(${idAtual}, '${p.nome}', ${p.preco})">
                                    <i class="bi bi-plus-lg"></i> Adicionar
                                </button>
                            </div>
                        </div>
                    </div>`;
            }).join('');
        }

    } catch (err) {
        console.error("Erro na API:", err);
        container.innerHTML = `<div class="alert alert-danger w-100">Erro ao carregar produtos. Verifique se a API está rodando.</div>`;
    } finally {
        // 2. IMPORTANTE: Libera a visualização da página independente de ter erro ou não
        document.body.style.display = "block";
    }
}

// 3. REGRAS DO CARRINHO
window.adicionarAoCarrinho = (id, nome, preco) => {
    const itemExistente = carrinho.find(i => i.idProduto === id);
    if (itemExistente) {
        itemExistente.quantidade++;
    } else {
        // Removido o erro 'Medical' que estava aqui
        carrinho.push({ idProduto: id, nome, preco, quantidade: 1 });
    }
    renderizarCarrinho();
};

window.removerItem = (index) => {
    carrinho.splice(index, 1);
    renderizarCarrinho();
};

function renderizarCarrinho() {
    const container = document.getElementById('itens-carrinho');
    const totalSpan = document.getElementById('total-carrinho');
    const btnFinalizar = document.getElementById('btn-finalizar');

    if (carrinho.length === 0) {
        container.innerHTML = '<p class="text-muted text-center py-3">Seu carrinho está vazio.</p>';
        btnFinalizar.disabled = true;
        totalSpan.innerText = "R$ 0,00";
        return;
    }

    btnFinalizar.disabled = false;
    let total = 0;

    container.innerHTML = carrinho.map((item, index) => {
        const subtotal = item.preco * item.quantidade;
        total += subtotal;
        return `
            <div class="d-flex justify-content-between align-items-center mb-2 p-2 bg-light rounded">
                <div>
                    <span class="fw-bold small">${item.quantidade}x</span> <span class="small">${item.nome}</span>
                </div>
                <div class="text-end">
                    <span class="small d-block fw-bold">R$ ${subtotal.toFixed(2)}</span>
                    <button class="btn btn-sm p-0 text-danger small" style="font-size:0.75rem" onclick="removerItem(${index})">Remover</button>
                </div>
            </div>
        `;
    }).join('');

    totalSpan.innerText = `R$ ${total.toFixed(2)}`;
}

// 4. FINALIZAR PEDIDO (ENVIO PARA API)
window.finalizarPedido = async () => {
    const nomeCliente = prompt("Informe o nome do cliente:");
    if (!nomeCliente) return;

    const pedidoData = {
        clienteNome: nomeCliente,
        numeroMesa: Math.floor(Math.random() * 15) + 1,
        itens: carrinho.map(i => ({
            idProduto: i.idProduto,
            quantidade: i.quantidade,
            precoUnitario: i.preco
        }))
    };

    try {
        const response = await fetch('/api/pedidos', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(pedidoData)
        });

        if (response.ok) {
            alert("Pedido enviado com sucesso para a API!");
            carrinho = [];
            renderizarCarrinho();
        } else {
            const erroMsg = await response.text();
            alert("Erro ao enviar pedido: " + erroMsg);
        }
    } catch (err) {
        alert("Erro de comunicação com o servidor.");
    }
};

// 5. LOGOUT
window.logout = () => {
    localStorage.removeItem("token_jwt");
    window.location.href = "login.html";
};

// Iniciar a listagem automaticamente
document.addEventListener('DOMContentLoaded', carregarProdutos);