// ==========================================
// 1. BLOQUEIO DE SEGURANÇA IMEDIATO
// ==========================================
const token = localStorage.getItem("token_jwt");

// Se não houver token ou for inválido, expulsa antes de carregar o resto
if (!token || token === "undefined" || token === "null") {
    alert("Acesso negado! Por favor, faça login.");
    window.location.href = "login.html";
}

let carrinho = [];

// ==========================================
// 2. CARREGAR PRODUTOS DA API
// ==========================================
async function carregarProdutos() {
    const container = document.getElementById('lista-produtos');

    try {
        const response = await fetch('/api/produtos', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        // Caso o token tenha expirado ou seja inválido na API
        if (response.status === 401) {
            alert("Sessão expirada. Faça login novamente.");
            logout();
            return;
        }

        const produtos = await response.json();

        if (!produtos || produtos.length === 0) {
            container.innerHTML = `<div class="alert alert-info w-100 text-center">Nenhum produto encontrado no cardápio.</div>`;
        } else {
            container.innerHTML = produtos.map(p => {
                // Tenta pegar o ID tanto como idProduto quanto id_produto (garantia)
                const idAtual = p.idProduto || p.id_produto || p.id;

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
        container.innerHTML = `<div class="alert alert-danger w-100">Erro ao carregar produtos. Verifique se o servidor está online.</div>`;
    } finally {
        // Torna o corpo da página visível após a verificação
        document.body.style.display = "block";
    }
}

// ==========================================
// 3. REGRAS DO CARRINHO
// ==========================================
window.adicionarAoCarrinho = (id, nome, preco) => {
    const itemExistente = carrinho.find(i => i.idProduto === id);

    if (itemExistente) {
        itemExistente.quantidade++;
    } else {
        carrinho.push({
            idProduto: id,
            nome: nome,
            preco: preco,
            quantidade: 1
        });
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
            <div class="d-flex justify-content-between align-items-center mb-2 p-2 bg-light rounded shadow-sm">
                <div>
                    <span class="fw-bold small">${item.quantidade}x</span> <span class="small">${item.nome}</span>
                </div>
                <div class="text-end">
                    <span class="small d-block fw-bold">R$ ${subtotal.toFixed(2)}</span>
                    <button class="btn btn-sm p-0 text-danger" style="font-size:0.7rem" onclick="removerItem(${index})">Remover</button>
                </div>
            </div>
        `;
    }).join('');

    totalSpan.innerText = `R$ ${total.toFixed(2)}`;
}

// ==========================================
// 4. FINALIZAR PEDIDO (ENVIO PARA API)
// ==========================================
window.finalizarPedido = async () => {
    const nomeCliente = prompt("Informe o nome para o pedido:");
    if (!nomeCliente) return;

    const pedidoData = {
        clienteNome: nomeCliente,
        numeroMesa: Math.floor(Math.random() * 20) + 1, // Simula uma mesa
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
            alert("🎉 Pedido enviado com sucesso!");
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

// ==========================================
// 5. LOGOUT
// ==========================================
window.logout = () => {
    localStorage.removeItem("token_jwt");
    window.location.href = "login.html";
};

// Iniciar a listagem automaticamente quando a página carregar
document.addEventListener('DOMContentLoaded', carregarProdutos);