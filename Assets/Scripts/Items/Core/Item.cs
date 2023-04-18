using Items.Data;

namespace Items.Core
{
    public abstract class Item
    {
        public ItemDescriptor Descriptor { get; }

        public abstract int Amount { get; }

        public Item(ItemDescriptor descriptor) => Descriptor = descriptor;

        public abstract void Use();

    }
}