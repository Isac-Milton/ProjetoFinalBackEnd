namespace LanchoneteAPI.Models;

public enum PerfilUsuario { Admin, Funcionario, Cliente }
public enum TipoDesconto { Percentual, ValorFixo }
public enum StatusPedido { Pendente, Confirmado, EmPreparo, ProntoParaEntrega, EmEntrega, Entregue, Cancelado }
public enum TipoPedido { Local, Delivery, Retirada }
public enum FormaPagamento { Dinheiro, Cartao, Pix }