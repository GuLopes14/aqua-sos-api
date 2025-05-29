package br.com.aquasos.config;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;

import br.com.aquasos.model.PedidoAgua;
import br.com.aquasos.model.PontoDistribuicao;
import br.com.aquasos.model.Usuario;
import br.com.aquasos.model.enums.StatusPedido;
import br.com.aquasos.model.enums.UserRole;
import br.com.aquasos.repository.PedidoAguaRepository;
import br.com.aquasos.repository.PontoDistribuicaoRepository;
import br.com.aquasos.repository.UsuarioRepository;
import jakarta.annotation.PostConstruct;

@Component
public class DatabaseSeeder {

        @Autowired
        private UsuarioRepository usuarioRepository;

        @Autowired
        private PontoDistribuicaoRepository pontoDistribuicaoRepository;

        @Autowired
        private PedidoAguaRepository pedidoAguaRepository;

        @Autowired
        private PasswordEncoder passwordEncoder;

        @PostConstruct
        public void init() {
                if (usuarioRepository.count() == 0) {
                        var usuarios = List.of(
                                        Usuario.builder()
                                                        .nome("João Solicitante")
                                                        .email("joao@aqua.com")
                                                        .password(passwordEncoder.encode("joao123"))
                                                        .role(UserRole.SOLICITANTE)
                                                        .build(),
                                        Usuario.builder()
                                                        .nome("Ana Voluntária")
                                                        .email("ana@aqua.com")
                                                        .password(passwordEncoder.encode("ana123"))
                                                        .role(UserRole.VOLUNTARIO)
                                                        .build(),
                                        Usuario.builder()
                                                        .nome("Carlos Solicitante")
                                                        .email("carlos@aqua.com")
                                                        .password(passwordEncoder.encode("carlos123"))
                                                        .role(UserRole.SOLICITANTE)
                                                        .build(),
                                        Usuario.builder()
                                                        .nome("Beatriz Voluntária")
                                                        .email("beatriz@aqua.com")
                                                        .password(passwordEncoder.encode("beatriz123"))
                                                        .role(UserRole.VOLUNTARIO)
                                                        .build(),
                                        Usuario.builder()
                                                        .nome("Lucas Solicitante")
                                                        .email("lucas@aqua.com")
                                                        .password(passwordEncoder.encode("lucas123"))
                                                        .role(UserRole.SOLICITANTE)
                                                        .build());
                        usuarioRepository.saveAll(usuarios);
                }

                if (pontoDistribuicaoRepository.count() == 0) {
                        var pontos = List.of(
                                        PontoDistribuicao.builder()
                                                        .nome("Ponto Central")
                                                        .endereco("Rua Principal, 100")
                                                        .cidade("São Paulo")
                                                        .capacidadeTotalLitros(10000)
                                                        .build(),
                                        PontoDistribuicao.builder()
                                                        .nome("Ponto Norte")
                                                        .endereco("Avenida Norte, 200")
                                                        .cidade("Guarulhos")
                                                        .capacidadeTotalLitros(8000)
                                                        .build(),
                                        PontoDistribuicao.builder()
                                                        .nome("Ponto Sul")
                                                        .endereco("Rua Sul, 300")
                                                        .cidade("Santo André")
                                                        .capacidadeTotalLitros(6000)
                                                        .build(),
                                        PontoDistribuicao.builder()
                                                        .nome("Ponto Leste")
                                                        .endereco("Avenida Leste, 400")
                                                        .cidade("Osasco")
                                                        .capacidadeTotalLitros(7000)
                                                        .build(),
                                        PontoDistribuicao.builder()
                                                        .nome("Ponto Oeste")
                                                        .endereco("Rua Oeste, 500")
                                                        .cidade("Barueri")
                                                        .capacidadeTotalLitros(9000)
                                                        .build());
                        pontoDistribuicaoRepository.saveAll(pontos);
                }

                if (pedidoAguaRepository.count() == 0) {
                        var usuarios = usuarioRepository.findAll();
                        var pontos = pontoDistribuicaoRepository.findAll();

                        var pedidos = List.of(
                                        PedidoAgua.builder()
                                                        .usuario(usuarios.get(0))
                                                        .ponto(pontos.get(0))
                                                        .quantidadeLitros(500)
                                                        .dataSolicitacao(java.time.LocalDate.now())
                                                        .status(StatusPedido.PENDENTE)
                                                        .build(),
                                        PedidoAgua.builder()
                                                        .usuario(usuarios.get(1))
                                                        .ponto(pontos.get(1))
                                                        .quantidadeLitros(1000)
                                                        .dataSolicitacao(java.time.LocalDate.now())
                                                        .status(StatusPedido.ENTREGUE)
                                                        .build(),
                                        PedidoAgua.builder()
                                                        .usuario(usuarios.get(2))
                                                        .ponto(pontos.get(2))
                                                        .quantidadeLitros(750)
                                                        .dataSolicitacao(java.time.LocalDate.now())
                                                        .status(StatusPedido.PENDENTE)
                                                        .build(),
                                        PedidoAgua.builder()
                                                        .usuario(usuarios.get(3))
                                                        .ponto(pontos.get(3))
                                                        .quantidadeLitros(1200)
                                                        .dataSolicitacao(java.time.LocalDate.now())
                                                        .status(StatusPedido.ENTREGUE)
                                                        .build(),
                                        PedidoAgua.builder()
                                                        .usuario(usuarios.get(4))
                                                        .ponto(pontos.get(4))
                                                        .quantidadeLitros(900)
                                                        .dataSolicitacao(java.time.LocalDate.now())
                                                        .status(StatusPedido.PENDENTE)
                                                        .build());
                        pedidoAguaRepository.saveAll(pedidos);
                }
        }
}