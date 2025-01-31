using UnityEngine;
using TMPro;

public class Encyclopedia : MonoBehaviour
{
    public TMP_Text description;
    public void Wizard()
    {
        description.text = "Poderoso hechizero capaz de crear portales en su beneficio. Su técnica requiere estudio y preparación.\n\nHabilidad: Crea un portal en el muro adyacente seleccionado permitiendo el movimiento a través de él. Desaparece al finalizar el turno.\n\nTiempo de recarga: 6 turnos.\n\nPasos iniciales: 6.";
    }
    public void Minotaur()
    {
        description.text = "Bestia mitad hombre mitad toro con gran capacidad muscular. Si bien parece una criatura torpe, efectivamente lo es.\n\nHabilidad: Aumenta la cantidad de pasos disponibles en 10. Al activarse remueve el estado de paralisis.\n\nTiempo de recarga: 4 turnos.\n\nPasos iniciales: 7.";
    }
    public void Oracle()
    {
        description.text = "Ser de insuperable visión. Conocido como \"Aquel que todo lo ve\"(o casi).\n\nHabilidad: Aumenta significativamente el rango de visión durante 3 turnos. Al activarse remueve el estado de cegado.\n\nTiempo de recarga: 5 turnos.\n\nPasos iniciales: 6.";
    }
    public void Warlock()
    {
        description.text = "Brujo no muerto, fuente de maldad. Por alguna razón es odiado por todos excepto sus semejantes.\n\nHabilidad: Maldice un camino aleatorio del mapa paralizando por 2 turnos al primero que lo pise (excepto otros brujos). Es inmune a todo tipo de trampas.\n\nTiempo de recarga: 1 turno.\n\nPasos iniciales: 7.";
    }
    public void DarkEntity()
    {
        description.text = "Entidad desconocida investigada por brujos y hechizeros. Se cree que puede fragmentar su alma y moverse entre dimensiones.\n\nHabilidad: Crea una marca en el camino actual a la que puede teletransportarse. La marca aporta visión durante su turno y no desaparecerá hasta que se use o cancele. No cura la paralisis.\n\nTiempo de recarga: 2 turnos.\n\nPasos iniciales: 8.";
    }
    public void Golem()
    {
        description.text = "Creación de los hechizeros para caseres domésticos. Una entidad oscura se fusionó con él.\n\nHabilidad: Crea un minigolem en al que puede teletransportarse. El minigolem aporta visión durante su turno y no desaparecerá hasta que se use o cancele. Además el minigolem es terco y se mueve en dirección opuesta al jugador. No cura la paralisis.\n\nTiempo de recarga: 2 turnos.\n\nPasos iniciales: 8.";
    }
    public void TrapBeard()
    {
        description.text= "Efecto: Paraliza al primer jugador que caiga sobre ella durante 3 turnos.";
    }
    public void TrapVision()
    {
        description.text= "Efecto: Ciega al primer jugador que caiga sobre ella durante 3 turnos.";
    }
    public void TrapCooldown()
    {
        description.text= "Efecto: Aumenta el tiempo de recarga restante de la habilidad del primer jugador que caiga sobre ella en 5 turnos. Si el jugador tiene la habilidad activa, la penalización se aplicará cuando esta finalice.";
    }
    public void Torch()
    {
        description.text="Efecto: Aumenta el rango de visión (algo menor que la habilidad de Oracle) durante 3 turnos. Al activarse remueve el efecto de cegado.";
    }
    public void Bomb()
    {
        description.text="Efecto: Destruye todos los muros en un radio de una casilla alrededor del jugador.";
    }
    public void Gema()
    {
        description.text="Efecto: Reduce el tiempo de recarga de la habilidad del jugador a 0. No conviene usarlo cuando la habilidad esté disponible.";
    }
    public void Portal()
    {
      description.text="Existen 4 ubicadas en cada extremo del mapa. Al activarse permite teletransportarse entre ellas ";
    }
    public void Rules()
    {
      description.text="Al iniciar partida cada jugador elige el personaje deseado. Gana el primero en encontrar el tesoro ubicado en la zona central. Solo durante su turno el jugador puede moverse si tiene pasos disponibles, usar su habilidad si no está inhabilitada, recoger/usar objetos y pasar cuando esté listo. Los controles están visibles en el hud desplegable durante la partida.";
    }
    public void Rules2()
    {
        description.text="A diferencia de los objetos, las trampas están ocultas hasta su activación. No se puede traspasar las paredes en condiciones normales. Al recoger un objeto no se puede recoger otro hasta que este se haya consumido o acabado su efecto. El orden de los turnos es el mismo que el orden al elegir personajes. Inicialmente todos tienen su habilidad en recarga por 3 turnos.";
    }
    public void Status()
    {
        description.text="Sin Cambios: Estado base. EL jugador al estar en este estado puede realizar una acción.\n\nMoviendo: El jugador se está moviendo y no se puede realizar otra acción hasta que esté en el estado SIN CAMBIOS.\n\nModo Selección: En este estado las teclas de movimineto se utilizan para seleccionar y no se puede realizar otra acción hasta salir de este estado.";
    }
    public void Status2()
    {
        description.text="Paralizado: Durante este estado el jugador no puede moverse. Puede usar su habilidad u objeto en caso de ser posible.\n\nCegado: Durante este estado la visión del jugador se reduce considerablemente.\n\n";
    }
}
