using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RecycleScrollView
{
    public class UICellSampleData
    {
        public int index;
        public string name;
    }

    [AddComponentMenu(nameof(RecycleScrollView) + "/" + nameof(UIRecycleViewCellSample))]
    public class UIRecycleViewCellSample : UIRecycleViewCell<UICellSampleData>
    {
        [SerializeField]
        private TextMeshProUGUI nIndex;
        [SerializeField]
        private TextMeshProUGUI txtName;

        public override void UpdateContent(UICellSampleData itemData)
        {
            nIndex.text = itemData.index.ToString();
            txtName.text = itemData.name;
        }

        public void onClickedButton()
        {
            Debug.Log(nIndex.text.ToString());
        }
    }
}