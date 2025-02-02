# Introducción

Este juego se creó usando Unity 6(6000.0.24f1).En la carpeta scripts ubicada en Assets están contenidos todos los scripts usados. En la carpeta prefabs están los prefabs junto con sus sprites. Los principales scripts son el TurnManager (lógica de los turnos), el MazeLogic(lógica de generación del mapa) y el Status(controla los diferentes estados de los jugadores ).El resto de script controla funciones más individuales pero igual de importantes tal es el caso de las habilidades, trampas, buffs, interfaz, etc.

# MazeLogic

En este script se genera el mapa usando un backtrackin y luego se llama a una serie de métodos para ir rellenándolo con las trampas ,buffs, etc.

## Generación aleatoria del mapa

Primero se crea una matriz de las dimensiones deseadas (impares y cuadrada) y se rellena de 1 representando las paredes. Luego se elige una posición al azar para empezar a "cavar".Estando en esta posición se verifica en las cuatro direcciones adyacentes (desordenadas,de ahi la aleatoriedad) si es posible cavar(es posible si está dentro de los limites y si dos casillas por delante hay pared), de serlo cava la casilla en esa dirección y analiza sus cuatro direcciones de manera recursiva llamando a este método y pasándole como casilla inicial la que se cavó recién. En caso contrario pasa a analizar la siguiente dirección. Esto funciona porque al completar todas las direcciones pasa a analizar la siguiente dirección del padre y esto garantiza que se genere un laberinto perfecto con un camino principal y sin obstrucciones.

## Generación del tesoro

Primero, el laberinto está compuesto por 33x33 excluyendo los bordes por lo que se puede dividir en 9 bloques de 11x11. EL tesoro se genera en una posición aleatoria del bloque central pero que sea la más equilibrada con respecto a los jugadores. Se hace un bfs desde cada jugador para saber los pasos necesarios para llegar a cada casilla .Revisa cada casilla del bloque central y se selecciona aquella que posea la minima diferencia entre el más lejano y el más cercano y que a su vez se encuentre lo más cercano al centro posible.

## Generación de trampas

Para ello se recorre cada bloque en orden aleatorio(excluyendo los bloques esquina) ,colocando una trampa del tipo aleatorio en una posición aleatoria válida(es valida si solo había camino) en el bloque actual antes de pasar al siguiente .Este bucle se sigue ejecutando hasta colocar todas las trampas deseadas. Antes de entrar se define el total de trampas deseadas de cada tipo.

### Trampas inevitables

Además de la generación de trampas, a cada player se le genera "su trampa",una trampa que no podrá evitar si intenta pasar el laberinto de manera ordinaria sin usar beneficios(a menos que otro se la coma primero).Para ello se hace un bfs desde el tesoro para encontrar el camino más corto desde cada jugador hasta él.Esto se logra a partir de cada jugador revisando la casilla con el menor número adyacente y guardando sus coordenadas en una lista, esto en bucle hasta llegar al tesoro.Ahora solo queda hacer instancia de una trampa de tipo aleatorio en la segunda mitad del camino para reducir las posibilidades de que una trampa caiga demásiado cerca del inicio.

## Buffs

Siguiendo lógica similar a las trampas se recorren los 4 bloques centrales (excluyendo esta vez al central) y se genera un buffo en una posición aleatoria válida hasta generar todas las deseadas.

## Travel points

Se genera uno en cada cada de los buffos pero asegurándose que esté en un rango de 3 casillas con el extremo.

# Turn Manager

Aquí se regula el cambio de turno, para ello al pasar voy sumando 1 a un conteo y aplico % con la cantidad total de jugadores obteniendo un ciclo que va recorriendo los index de cada jugador.EL jugador con el turno actual es el que lleva por tag este index+1(tag que se rellena al generarse el mapa).Para dar la ilusión de cambiar de turno inicialmente todos los jugadores tienen su componente de luz y movimiento desactivadas y solo se activa las componentes del jugador que le toca el turno.Estas funciones están separadas en dos métodos uno para iniciar y otro para finalizar.Además al iniciar turno se llama al método Reset Move para restablecer los movimientos disponibles;se descuenta turno de los cooldown en caso de existir y se suma 1 al contador de turno actual de cada jugador usado para regular la duración de ciertos efectos.También se actualiza los textos que se muestran en la UI.

# Status

Controla los estados de cada jugador mediante una serie de variables booleanas.Además contiene la lógica de la activación de buffs de manera similar a un inventario.

# Habilidades

Cada script de habilidad comparten lógica similar. Primero debe cumplir requisitos para activarse tales como tenerla disponible ,no estar en pausa, tener el turno activo.....
Ahora depende cada habilidad activa su función.Existen algunas simples que no requieren mucha explicación como la de vision o movimientos,a continuación se explicaran las más complejas.

## wizard

Al activarse se entra en el modo selección y se deshabilita el movimiento para usar las teclas WSAD para elegir donde ira el portal.si la posición deseada es valida se genera el portal y se declara un 0 en esa posición en la matriz del laberinto para habilitar el movimiento a traves de ella.Al pasar turno se remueve este efecto y se restablece el 1 de la pared.

## Golem y DarkEntity

Tienen lógica similar.Se instancia una marca en la posición actual y al activarse el jugador cambia su transform al de la marca.EN el caso del golem la marca se mueve en dirección opuesta al jugador.Esto restando la posición del golem y a la que se movió para tener la dirección,luego se le resta al objetivo de la marca la dirección y hago que se mueva hasta esta posición usando un Move Towards.

## Warlock

Primero para conseguir su inmunidad a las trampas,cada trampa tienen dentro de sus condiciones de activación que el objetivo no posea esta habilidad.Para generar una trampa en una posición aleatoria basta con generar posiciones aleatorias y verificar que sean validas.

# Movimiento

Guarda la mecánica de movimiento por celdas.Recibe un input para determinar la dirección y con el Move Toward se ejecuta el movimiento si la posición es válida.Además para dar el efecto de cuadricula inhibe otra entrada hasta que el movimiento haya concluido(que ocurre al llegar al centro de la celda).Con cada paso se descuenta un movimiento disponible y al llegar a 0 se desactiva esta componente.

# Beneficios y trampas

Cada buff posee su respectivo script que al reunir las condiciones de activación pone en true el indicador correspondiente en el Status de cada player.De manera similar ocurre con las trampas,al activarse aplican la penalización correspondiente y modifican el status para la lógica de los debuff.

# Travel Points

La lógica que sigue para cambiar entre portales es similar a la habilidad del wizard.AL usar WSDA cambia el transform del jugador al de los portales correspondientes predefinidos en una lista y al confirmar reanuda el curso normal del juego.

# UI

En esta carpeta están los scripts que controlan la interfaz visual como los botones y textos aprovechando las facilidades que aporta el unity.
