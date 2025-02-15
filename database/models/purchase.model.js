import mongoose from 'mongoose';

const purchaseSchema = new mongoose.Schema({
    startDate: {
        type: Date,
        required: true,
    },
    endDate: {
        type: Date,
        required: true,
    },
    quantity: {
        type: Number,
        required: true,
    },
    status: {
        type: String,
        required: true,
    },
    productId: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'Product', // Reference to Product model
        required: true
    }
}, {
    timestamps: true
});

const Purchase = mongoose.model('Purchase', purchaseSchema);

export default Purchase;
