package br.com.aquasos.config;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import br.com.aquasos.model.PontoDistribuicao;
import br.com.aquasos.model.Usuario;
import br.com.aquasos.model.enums.TipoUsuario;
import br.com.aquasos.repository.PontoDistribuicaoRepository;
import br.com.aquasos.repository.UsuarioRepository;
import jakarta.annotation.PostConstruct;

@Component
public class DatabaseSeeder {

    @Autowired
    private UsuarioRepository usuarioRepository;

    @Autowired
    private PontoDistribuicaoRepository pontoDistribuicaoRepository;

    @PostConstruct
    public void init() {
        if (usuarioRepository.count() == 0) {
            var usuarios = List.of(
                    Usuario.builder()
                            .nome("João Solicitante")
                            .email("joao@aqua.com")
                            .password("joao123")
                            .tipoUsuario(TipoUsuario.SOLICITANTE)
                            .build(),
                    Usuario.builder()
                            .nome("Ana Voluntária")
                            .email("ana@aqua.com")
                            .password("ana123")
                            .tipoUsuario(TipoUsuario.VOLUNTARIO)
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
                            .build());
            pontoDistribuicaoRepository.saveAll(pontos);
        }
    }
}