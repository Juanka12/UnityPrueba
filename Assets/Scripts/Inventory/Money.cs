public class Money
{
    public int value{get; set;}

    public void AddMoney(int newValue){
        value += newValue;
    }

    public void RestaMoney(int newValue){
        value -= newValue;
    }
}
