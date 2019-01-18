//De fleste scripts skal have adgang til forskellige unity-biblioteker. Engine er som oftest obligatorisk
using UnityEngine;

//Namespaces sørger for at variable og klassenavne ikke er globale 
namespace Shoot {
public class Shoot : MonoBehaviour{

	//En public variabel kan ses i inspectoren - kig selv: scriptet forventer ligesom at få en reference til en prefab den kan skyde med 
	public GameObject bulletPrefab;

	//Start kører inden spillet går i gang - Update kører imens  
	private void Start(){

	}

	void Update(){

		//Hvis der trykkes på tasten k, kaldes funktionen fire()
		if (Input.GetKeyDown("k")){
			Fire();
		}
	}

	//Og fire er her..
	void Fire()
	{
		// Nu kan vi bruge referencen fra vores public variabel til at initialisere en ny kugle
		// Bemærk ordet transform- det henviser altid til den transform scriptet ligger på
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			transform.position,
			transform.rotation
		);

		// Giv kuglen noget fart 
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20.0f;
	}

}
}

/*OPGAVER
 * 
 * Vi skal bruge dette script til at få fyret nogle troværdige kugler af, når spilleren trykker på en tast.
 * Men vi går langsomt frem, så i får stavet jer igennem kode syntaksen stille og roligt
 * 
 * Prøv at tilføje en debug besked hver gang spilleren skyder
 * https://docs.unity3d.com/ScriptReference/MonoBehaviour-print.html
 * 
 * Lige nu ligger kuglerne lidt underligt langs jorden. 
 * Se om du kan finde ud af at ændre kuglens y position i udgangspunktet så man skyder højere på banen. 
 * https://docs.unity3d.com/ScriptReference/Transform-position.html
 * 
 * - lav også en public variabel til affyringshøjden, så du kan styre den direkte i editoren
 * 
 * Kuglerne skyder lidt langsomt - prøv at sæt farten op (kig på tallet vi ganger med)
 * - kan du lave en public variabel, så man kan sætte kuglens affyringshastighed direkte i editoren?
 * https://docs.unity3d.com/Manual/VariablesAndTheInspector.html
 * 
 * Prøv at brug metoden Destroy til at fjerne affyrede kugler efter et par sekunder 
 * 
 * Prøv at få noget lyd på kuglerne - kig i det script der allerede ligger på FPScontrolleren - du skal bruge det som har med audio at gøre. 
 * Du kan finde flere lyde her: http://soundbible.com/
 * 
 * 
 * Allerede færdig? Prøv at lave et pistolobjekt som vises i first person, og som kuglerne affyres fra. 
*/