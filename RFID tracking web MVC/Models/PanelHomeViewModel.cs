namespace RFID_tracking_web_MVC.Models {
    public class PanelHomeViewModel {

        public List<PanelHomeElement> list = new List<PanelHomeElement>();
    }

    public class PanelHomeElement {
        public string WeaponName { get; set; }
        public string ShooterName { get; set; }
        public WeaponStatus WeaponStatus { get; set; }
        public Guid WeaponId { get; set; }
    }
}
