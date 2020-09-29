#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Crg+gQq4OZmaOTg7ODg7OAo3PDNUXhpZVVReU05TVVRJGlVcGk9JX1ZfGnNUWRQLHAoePDlvPjEpJ3tKPdZHA7mxaRrpAv6LhaB1MFHFEcYlv7m/IaMHfQ3Ik6F6tBbui6oo4o8Al841NDqoMYsbLBRO7wY34VgsELxyvM03Ozs/PzoKWAsxCjM8OW8Mo3YXQo3XtqHmyU2hyEzoTQp1+2hfVlNbVFlfGlVUGk5SU0kaWV9ICis8OW8+MCkwe0pKVl8ac1RZFAtIW1lOU1lfGklOW05fV19UTkkUCiWr4SR9atE/12RDvhfRDJhtdm/WMhE8Oz8/PTg7LCRSTk5KSQAVFU1TXFNZW05TVVQae09OUlVIU05DC4TOSaHU6F418UN1DuKYBMNCxVHyRXuSosPr8FymHlEr6pmB3iEQ+SUJDGAKWAsxCjM8OW8+PCk4b2kLKbouEepTfa5MM8TOUbcUepzNfXdFGlVcGk5SXxpOUl9UGltKSlZTWVscCh48OW8+MSkne0pKVl8aeV9ITj48KThvaQspCis8OW8+MCkwe0pK4wxF+71v452jgwh4weLvS6REm2ixI7Pkw3FWzz2RGAo40iIEwmoz6UpWXxpoVVVOGnl7CiQtNwoMCg4IkZlLqH1pb/uVFXuJwsHZSvfcmXZOU1xTWVtOXxpYQxpbVEMaSltITkMaW0lJT1dfSRpbWVlfSk5bVFlfQAq4O0wKNDw5byc1OzvFPj45ODs8OW8nND4sPi4R6lN9rkwzxM5Rtw8ICw4KCQxgLTcJDwoICgMICw4Kiwpi1mA+CLZSibUn5F9JxV1kX4YaeXsKuDsYCjc8MxC8crzNNzs7OwccXRqwCVDNN7j15NGZFcNpUGFekuZEGA/wH+/jNexR7pgeGSvNm5a1Sbta/CFhMxWoiMJ+cspaAqQvz05SVUhTTkMLLAouPDlvPjkpN3tKf0QldlFqrHuz/k5YMSq5e70JsLv6WQlNzQA9FmzR4DUbNOCASSN1j14PGS9xL2Mnia7NzKak9WqA+2JqGltUXhpZX0hOU1xTWVtOU1VUGkoyZAq4Oys8OW8nGj64OzIKuDs+CkpWXxp5X0hOU1xTWVtOU1VUGntPHtjR641K5TV/2x3wy1dC192PLS24Ozo8MxC8crzNWV4/Owq7yAoQPBR6nM19d0UyZAolPDlvJxk+Igosc+JMpQkuX5tNrvMXODk7OjuZuDsVCrv5PDIRPDs/Pz04OAq7jCC7iT86Obg7NToKuDswOLg7Ozreq5MzFhpZX0hOU1xTWVtOXxpKVVZTWUONIYepeB4oEP01J4x3pmRZ8nG6La+kQDaefbFh7iwNCfH+NXf0LlPr8yNIz2c070VlocgfOYBvtXdnN8s8CjU8OW8nKTs7xT4/Cjk7O8UKJzWnB8kRcxIg8sT0j4M042Qm7PEHY50/M0YtemwrJE7pjbEZAX2Z71UsCi48OW8+OSk3e0pKVl8aaFVVTlhWXxpJTltUXltIXhpOX0hXSRpbXbUyjhrN8ZYWGlVKjAU7CraNefU3PDMQvHK8zTc7Oz8/Ojm4Ozs6Zk1NFFtKSlZfFFlVVxVbSkpWX1lbapCw7+DexuozPQ2KT08b");
        private static int[] order = new int[] { 0,43,3,51,49,23,43,48,38,9,46,15,35,15,50,17,38,32,41,46,51,58,38,43,24,39,40,28,30,48,40,32,56,54,55,43,39,57,51,45,54,49,53,55,56,54,57,50,56,59,59,57,56,57,56,55,57,58,59,59,60 };
        private static int key = 58;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
